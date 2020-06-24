namespace RSoftware.Unity.PublisherApi.Client.Models.Publisher
{
    using Newtonsoft.Json;

    public partial class LatestInfoList
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug_v2")]
        public string SlugV2 { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("overlay")]
        public object Overlay { get; set; }
    }

}
