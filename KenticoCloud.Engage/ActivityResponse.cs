﻿using Newtonsoft.Json;

namespace KenticoCloud.Engage
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
    }
}
