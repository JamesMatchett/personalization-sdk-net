using System;
using System.Collections.Generic;
using System.Net;
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
            ValidateIdParameter(uid);

            string sid = _randomIdGenerator.Generate();

            var postContent =new Dictionary<string, string>
            {
                { "uid", uid },
                { "sid", sid }
            };

            await PerformTrackingRequestAsync($"{VisitorApiRoutePrefix}/{_projectId}/session", postContent);

            return sid;
        }

        /// <summary>
        /// Records custom activity for the visitor in the given session. 
        /// Activity of the given <paramref name="activityName"/> must be defined in Kentico Cloud application otherwise activity won't be logged.
        /// </summary>
        /// <param name="uid">ID of the tracked visitor</param>
        /// <param name="sid">ID of the session this activity belongs to</param>
        /// <param name="activityName">Name of the activity</param>
        public async Task RecordActivity(string uid, string sid, string activityName)
        {
            ValidateIdParameter(uid);
            ValidateIdParameter(sid);
            if (string.IsNullOrEmpty(activityName))
            {
                throw new ArgumentException("message", nameof(activityName));
            }

            var postContent = new Dictionary<string, string>
            {
                { "uid", uid },
                { "sid", sid },
                { "name", activityName }
            };

            await PerformTrackingRequestAsync($"{VisitorApiRoutePrefix}/{_projectId}/activity", postContent);
        }

        /// <summary>
        /// Records visitor's email to its contact profile.
        /// </summary>
        /// <param name="uid">ID of the tracked visitor</param>
        /// <param name="sid">ID of the session this activity belongs to</param>
        /// <param name="email"></param>
        /// <returns>Status code for the performed request</returns>
        public async Task<HttpStatusCode> RecordVisitorEmail(string uid, string sid, string email)
        {
            ValidateIdParameter(uid);
            ValidateIdParameter(sid);
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email must be set", nameof(email));
            }

            var postContent = new Dictionary<string, string>
            {
                { "uid", uid },
                { "sid", sid },
                { "email", email }
            };

            return await PerformTrackingRequestAsync($"{VisitorApiRoutePrefix}/{_projectId}/contact", postContent);
        }

        private async Task<HttpStatusCode> PerformTrackingRequestAsync(string url, Dictionary<string, string> requestContent)
        {
            using (var response = await _httpClient.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json")))
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new PersonalizationException(response.StatusCode, responseBody);
                }

                return response.StatusCode;
            }
        }

        private void ValidateIdParameter(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentException("Uid must be set.", nameof(uid));
            }

            if (!Regex.IsMatch(uid, @"^[a-zA-Z0-9]{16}$"))
            {
                throw new ArgumentException("Uid format is invalid.");
            }
        }
    }
}
