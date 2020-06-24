namespace RSoftware.Unity.PublisherApi.Client.Models.Publisher
{
    using Newtonsoft.Json;

    public partial class PublisherInfoKeyImage
    {
        [JsonProperty("small_v2")]
        public string Small { get; set; }

        [JsonProperty("big_v2")]
        public string Big { get; set; }
    }
}
