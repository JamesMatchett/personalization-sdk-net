using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KenticoCloud.Personalization
{
    /// <summary>
    /// Class for querying Kentico Cloud Personalization API.
    /// </summary>
    public class PersonalizationClient
    {
        private readonly HttpClient _httpClient;
        private const string VisitorApiRoutePrefix = "v1/visitor";
        private const string SegmentApiRoutePrefix = "v1/segment";

        /// <summary>
        /// Client constructor for production API.
        /// </summary>
        /// <param name="accessToken"></param>
        public PersonalizationClient(string accessToken)
            : this("https://engage-api.kenticocloud.com", accessToken)
        {
        }

        /// <summary>
        /// Client constructor.
        /// </summary>
        /// <param name="endpointUri">Root url of API endpoint.</param>
        /// <param name="accessToken">Your personalization API key. It can be found on Kentico Cloud Developer page.</param>
        public PersonalizationClient(string endpointUri, string accessToken)
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri(endpointUri) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        }

        /// <summary>
        /// Gets segments in which user specified by <paramref name="uid"/> currently is.
        /// </summary>
        /// <param name="uid">User ID.</param>
        public async Task<SegmentsResponse> GetVisitorSegmentsAsync(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentException("Uid must be set.", nameof(uid));
            }
            using (var response = await _httpClient.GetAsync($"{VisitorApiRoutePrefix}/{uid}/segments"))
            {
                return await DeserializeContent<SegmentsResponse>(response);
            }
        }

        /// <summary>
        /// Gets list of visitors who are in segment specified by <paramref name="codename"/>.
        /// </summary>
        /// <param name="codename">Codename of segment.</param>
        /// <returns></returns>
        public async Task<List<string>> GetVisitorsInSegmentAsync(string codename)
        {
            if (string.IsNullOrEmpty(codename))
            {
                throw new ArgumentException("Codename must be set.", nameof(codename));
            }

            var uids = new List<string>();
            var nextLink = $"{SegmentApiRoutePrefix}/{codename}/visitors";
            while (nextLink != null)
            {
                using (var response = await _httpClient.GetAsync(nextLink))
                {
                    nextLink = response.Headers.TryGetValues("Link", out var links) ? LinkHeader.LinksFromHeader(links.FirstOrDefault())?.NextLink : null;

                    var content = await DeserializeContent<VisitorsResponse>(response);

                    uids.AddRange(content.Visitors.Select(visitor => visitor.Uid));
                }
            }
            return uids;
        }

        private async Task<T> DeserializeContent<T>(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(responseBody);
            }

            throw new PersonalizationException(response.StatusCode, responseBody);
        }
    }
}
