using System;
using Newtonsoft.Json;

namespace KenticoCloud.Personalization
{
    /// <summary>
    /// Represents result of user's activity summary.
    /// </summary>
    public class ActivitySummaryResponse
    {
        /// <summary>
        /// Total number of user's sessions.
        /// </summary>
        [JsonProperty("totalSessions")]
        public int TotalSessions { get; set; }

        /// <summary>
        /// Duration of activity from first session to last session.
        /// </summary>
        [JsonProperty("activityTimeSpan")]
        public TimeSpan ActivityTimeSpan { get; set; }

        /// <summary>
        /// Avarage number of actions in session.
        /// </summary>
        [JsonProperty("avgSessionActions")]
        public int AverageActionsInSession { get; set; }
    }
}
