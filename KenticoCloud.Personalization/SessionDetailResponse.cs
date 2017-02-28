using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace KenticoCloud.Personalization
{
    /// <summary>
    /// Represents session detail response.
    /// </summary>
    public class SessionDetailResponse
    {
        /// <summary>
        /// Start of the session.
        /// </summary>
        [JsonProperty("started")]
        public DateTimeOffset? Started { get; set; }

        /// <summary>
        /// Origin of the session.
        /// </summary>
        [JsonProperty("origin")]
        public string Origin { get; set; }

        /// <summary>
        /// Actions whithin this session.
        /// </summary>
        [JsonProperty("actions")]
        public List<SessionAction> Actions { get; set; } = new List<SessionAction>();
    }
}
