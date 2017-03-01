using System.Linq;
using Microsoft.AspNetCore.Http;

namespace KenticoCloud.Personalization.MVC
{
    /// <summary>
    /// Extension methods to provide access to Kentico Cloud Personalization IDs
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Gets the current UID from http request.
        /// </summary>
        /// <param name="request">Request from which the UID is parsed.</param>
        public static string GetCurrentPersonalizationUid(this HttpRequest request)
        {
            var cookieKey = request.Cookies.Keys.FirstOrDefault(x => x.StartsWith("k_e_id"));
            return !string.IsNullOrEmpty(cookieKey)
                ? request.Cookies[cookieKey]?.Split('.').FirstOrDefault()
                : null;
        }

        /// <summary>
        /// Gets the current SID from http request.
        /// </summary>
        /// <param name="request">Request from which the SID is parsed.</param>
        public static string GetCurrentPersonalizationSid(this HttpRequest request)
        {
            var cookieKey = request.Cookies.Keys.FirstOrDefault(x => x.StartsWith("k_e_ses"));
            return !string.IsNullOrEmpty(cookieKey)
                ? request.Cookies[cookieKey]?.Split('.').LastOrDefault()
                : null;
        }
    }
}