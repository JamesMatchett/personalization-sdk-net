using Newtonsoft.Json;

namespace KenticoCloud.Personalization
{
    /// <summary>
    /// Represents session's action.
    /// </summary>
    public class SessionAction
    {
        /// <summary>
        /// Type of action.
        /// </summary>
        [JsonProperty("type")]
        public ActionTypeEnum ActionType { get; set; }

        /// <summary>
        /// Url of page where action occured.
        /// </summary>
        [JsonProperty("pageUrl")]
        public string PageUrl { get; set; }
    }
}