using Newtonsoft.Json;

namespace KenticoCloud.Personalization
{
    /// <summary>
    /// Represents segment.
    /// </summary>
    public class Segment
    {
        /// <summary>
        /// Name of the segment.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
