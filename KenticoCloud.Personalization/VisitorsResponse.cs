using Newtonsoft.Json;

namespace KenticoCloud.Personalization
{
    internal class VisitorsResponse
    {
        [JsonProperty("visitors")]
        public Visitor[] Visitors { get; set; }
    }
}
