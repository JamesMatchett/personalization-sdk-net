using Newtonsoft.Json;

namespace KenticoCloud.Personalization
{
    internal class Visitor
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }
    }
}
