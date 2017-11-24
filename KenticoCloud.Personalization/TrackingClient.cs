using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KenticoCloud.Personalization
{
    /// <summary>
    /// Class for tracking visitors and activities in Kentico cloud using the tracker API.
    /// </summary>
    public class TrackingClient
    {
        private readonly Guid _projectId;
        private readonly HttpClient _httpClient;
        private readonly RandomIdGenerator _randomIdGenerator = new RandomIdGenerator();

        private const string VisitorApiRoutePrefix = "api/v1/track";

        /// <summary>
        /// Client constructor for production API.
        /// </summary>
        /// <param name="projectId">ID of your project, can be found in the Kentico cloud application</param>
        public TrackingClient(Guid projectId) : this("https://engage-ket.kenticocloud.com", projectId)
        {
        }

        internal TrackingClient(string endpointUri, Guid projectId)
        {
            _projectId = projectId;

            _httpClient = new HttpClient { BaseAddress = new Uri(endpointUri) };
        }

        /// <summary>
        /// Records start of a new session for the visitor and returns ID of the recorded session.
        /// </summary>
        /// <param name="uid">ID of the tracked visitor</param>
        /// <returns>ID of the created session</returns>
        public async Task<string> RecordNewSession(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentException("Uid must be set.", nameof(uid));
            }

            if (!Regex.IsMatch(uid, @"^[a-zA-Z0-9]{16}$"))
            {
                throw new ArgumentException("Uid format is invalid.");
            }

            string sid = _randomIdGenerator.Generate();

            await PerformTrackingRequestAsync($"{VisitorApiRoutePrefix}/{_projectId}/session", uid, sid);

            return sid;
        }


        private async Task PerformTrackingRequestAsync(string url, string uid, string sid)
        {
            var content = new Dictionary<string, string>
            {
                { "uid", uid },
                { "sid", sid }
            };

            using (var response = await _httpClient.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")))
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new PersonalizationException(response.StatusCode, responseBody);
                }
            }
        }
    }
}
