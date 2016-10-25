using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KenticoCloud.Engage
{
    /// <summary>
    /// Class for querying Engage API.
    /// </summary>
    public class EngageClient
    {
        private readonly HttpClient _httpClient;
        private const string _visitorApiRoutePrefix = "v1/visitor";

        /// <summary>
        /// Client consturctor for production API.
        /// </summary>
        /// <param name="accessToken"></param>
        public EngageClient(string accessToken) 
            : this("https://engage-api.kenticocloud.com", accessToken)
        {   
        }

        /// <summary>
        /// Client constructor.
        /// </summary>
        /// <param name="endpointUri">Root url of API endpoint.</param>
        /// <param name="accessToken">Your personalization API key. It can be found on Engage application settings page.</param>
        public EngageClient(string endpointUri, string accessToken)
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri(endpointUri) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        }

        /// <summary>
        /// Gets the most usual location from which the current contact connects to your website. 
        /// </summary>
        /// <param name="uid">User ID.</param>
        public async Task<LocationResponse> GetVisitorUsualLocationAsync(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentException("Uid must be set.", nameof(uid));
            }

            return await GetResponseAsync<LocationResponse>($"{_visitorApiRoutePrefix}/{uid}/usuallocation");
        }

        /// <summary>
        /// Gets the current location from which the current contact is connected to your website.
        /// </summary>
        /// <param name="uid">User ID.</param>
        /// <param name="sid">Session ID.</param>
        public async Task<LocationResponse> GetCurrentLocationAsync(string uid, string sid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentException("Uid must be set.", nameof(uid));
            }

            if (string.IsNullOrEmpty(sid))
            {
                throw new ArgumentException("Sid must be set.", nameof(sid));
            }

            return await GetResponseAsync<LocationResponse>($"{_visitorApiRoutePrefix}/{uid}/currentlocation?sid={sid}");
        }

        /// <summary>
        /// Gets information about the current contact's first visit of your website.
        /// </summary>
        /// <param name="uid">User ID.</param>
        public async Task<VisitResponse> GetFirstVisitAsync(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentException("Uid must be set.", nameof(uid));
            }

            return await GetResponseAsync<VisitResponse>($"{_visitorApiRoutePrefix}/{uid}/firstvisit");
        }

        /// <summary>
        /// Gets information about the current contact's last visit of your website (i.e. the visit before the current visit).
        /// </summary>
        /// <param name="uid">User ID.</param>
        public async Task<VisitResponse> GetLastVisitAsync(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentException("Uid must be set.", nameof(uid));
            }

            return await GetResponseAsync<VisitResponse>($"{_visitorApiRoutePrefix}/{uid}/lastvisit");
        }

        /// <summary>
        /// Gets a summary of the current contact's overall activity on your website.
        /// </summary>
        /// <param name="uid">User ID.</param>
        public async Task<ActivitySummaryResponse> GetActivitySummaryAsync(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentException("Uid must be set.", nameof(uid));
            }

            return await GetResponseAsync<ActivitySummaryResponse>($"{_visitorApiRoutePrefix}/{uid}/activitysummary");
        }

        /// <summary>
        /// Gets information about the current contact's session. 
        /// </summary>
        /// <param name="uid">User ID.</param>
        /// <param name="sid">Session ID.</param>
        public async Task<SessionDetailResponse> GetCurrentSessionAsync(string uid, string sid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentException("Uid must be set.", nameof(uid));
            }

            if (string.IsNullOrEmpty(sid))
            {
                throw new ArgumentException("Sid must be set.", nameof(sid));
            }

            return await GetResponseAsync<SessionDetailResponse>($"{_visitorApiRoutePrefix}/{uid}/currentsession?sid={sid}");
        }

        /// <summary>
        /// Gets information whether the current contact performed a specified custom action.
        /// </summary>
        /// <param name="uid">User ID.</param>
        /// <param name="actionType">Type of action.</param>
        public async Task<ActivityResponse> GetVisitorActionsAsync(string uid, ActionTypeEnum actionType)
        {
            return await GetVisitorActionsAsync(uid, actionType, null);
        }

        /// <summary>
        /// Gets information whether the current contact performed a specified custom action with specified details.
        /// </summary>
        /// <param name="uid">User ID.</param>
        /// <param name="actionType">Type of action.</param>
        /// <param name="details">Details of action.</param>
        public async Task<ActivityResponse> GetVisitorActionsAsync(string uid, ActionTypeEnum actionType, ActionDetails details)
        {
            if (string.IsNullOrEmpty(uid))
            {
                throw new ArgumentException("Uid must be set.", nameof(uid));
            }
            
            var url = new StringBuilder($"{_visitorApiRoutePrefix}/{uid}/visitoractions?type={(int)actionType}");

            if (details?.DaysAgo != null)
            {
                url.Append($"&daysAgo={details.DaysAgo.Value}");
            }

            if (details?.PageUrl != null)
            {
                url.Append($"&pageUrl={details.PageUrl}");
            }

            if (details?.CustomActionName != null)
            {
                url.Append($"&customActionName={details.CustomActionName}");
            }

            return await GetResponseAsync<ActivityResponse>(url.ToString());
        }

        private async Task<T> GetResponseAsync<T>(string url)
        {
            using (var response = await _httpClient.GetAsync(url))
            {
                var responseBody = await response.Content.ReadAsStringAsync();
    
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(responseBody);
                }

                throw new EngageException(response.StatusCode, responseBody);
            }
        }
    }
}
