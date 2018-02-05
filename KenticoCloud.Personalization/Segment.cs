using Newtonsoft.Json;

namespace KenticoCloud.Personalization
{
    /// <summary>
    /// Represents segment.
    /// </summary>
    public class Segment
    {
        /// <summary>
        /// Codename of the segment.
        /// </summary>
        [JsonProperty("codename")]
        public string Codename { get; set; }
    }
}
