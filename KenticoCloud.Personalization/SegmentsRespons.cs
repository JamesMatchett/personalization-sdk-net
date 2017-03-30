using Newtonsoft.Json;

namespace KenticoCloud.Personalization
{
    /// <summary>
    /// Represents collection of segments.
    /// </summary>
    public class SegmentsResponse
    {
        /// <summary>
        /// Collection of segments.
        /// </summary>
        [JsonProperty("segments")]
        public Segment[] Segments { get; set; }
    }
}
