using System.Linq;
using System.Web;

namespace KenticoCloud.Personalization.MVC
{
    /// <summary>
    /// Extension methods to provide access to Kentico Cloud Personalization IDs
    /// </summary>
    public static class HttpRequestBaseExtensions
    {
        /// <summary>
        /// Gets the current UID from http request.
        /// </summary>
        /// <param name="request">Request from which the UID is parsed.</param>
        public static string GetCurrentPersonalizationUid(this HttpRequestBase request)
        {
            var cookieKey = request.Cookies.AllKeys.FirstOrDefault(x => x.StartsWith("k_e_id"));
            return !string.IsNullOrEmpty(cookieKey)
                ? request.Cookies[cookieKey]?.Value.Split('.').FirstOrDefault()
                : null;
        }

        /// <summary>
        /// Gets the current SID from http request.
        /// </summary>
        /// <param name="request">Request from which the SID is parsed.</param>
        public static string GetCurrentPersonalizationSid(this HttpRequest request)
        {
            var cookieKey = request.Cookies.AllKeys.FirstOrDefault(x => x.StartsWith("k_e_ses"));
            return !string.IsNullOrEmpty(cookieKey)
                ? request.Cookies[cookieKey]?.Value.Split('.').LastOrDefault()
                : null;
        }
    }
}