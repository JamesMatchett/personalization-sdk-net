using Newtonsoft.Json;

namespace KenticoCloud.Engage
{
    /// <summary>
    /// Represents response of location.
    /// </summary>
    public class LocationResponse
    {
        /// <summary>
        /// City.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// State.
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Country.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
