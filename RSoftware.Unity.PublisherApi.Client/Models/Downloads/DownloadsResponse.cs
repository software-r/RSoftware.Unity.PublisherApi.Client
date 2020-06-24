using Newtonsoft.Json;

namespace RSoftware.Unity.PublisherApi.Client.Models.Downloads
{
    public partial class DownloadsResponse
    {
        [JsonProperty("aaData")]
        public string[][] Payload { get; set; }

        [JsonProperty("result")]
        public PackageLink[] Links { get; set; }
    }
}
