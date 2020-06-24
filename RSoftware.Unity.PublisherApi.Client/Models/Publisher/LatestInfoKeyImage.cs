namespace RSoftware.Unity.PublisherApi.Client.Models.Publisher
{
    using Newtonsoft.Json;

    public partial class LatestInfoKeyImage
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("icon75")]
        public string Icon75 { get; set; }

        [JsonProperty("icon25")]
        public string Icon25 { get; set; }
    }
}
