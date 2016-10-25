using Newtonsoft.Json;
using System;

namespace KenticoCloud.Engage
{
    /// <summary>
    /// Represents user's visit response.
    /// </summary>
    public class VisitResponse
    {
        /// <summary>
        /// Date and time of the visit.
        /// </summary>
        [JsonProperty("visitDateTime")]
        public DateTimeOffset VisitDateTime { get; set; }

        /// <summary>
        /// Origin of the visit.
        /// </summary>
        [JsonProperty("origin")]
        public string Origin { get; set; }
    }
}
