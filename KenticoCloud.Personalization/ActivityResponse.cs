using System;
using Newtonsoft.Json;

namespace KenticoCloud.Personalization
{
    /// <summary>
    /// Represents response of user's activity.
    /// </summary>
    public class ActivityResponse
    {
        /// <summary>
        /// True whether specified activity occured.
        /// </summary>
        [JsonProperty("activity")]
        public bool Activity { get; set; }

        /// <summary>
        /// Date and time when last activity occured.
        /// </summary>
        [JsonProperty("lastOccurrence")]
        public DateTimeOffset? LastOccurence { get; set; }
    }
}
