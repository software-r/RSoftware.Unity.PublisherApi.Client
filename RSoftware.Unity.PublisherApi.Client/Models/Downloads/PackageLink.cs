namespace RSoftware.Unity.PublisherApi.Client.Models.Downloads
{
    using Newtonsoft.Json;

    public partial class PackageLink
    {
        [JsonProperty("short_url")]
        public string ShortUrl { get; set; }
    }
}
