using Newtonsoft.Json;

namespace RSoftware.Unity.PublisherApi.Client.Models.Downloads
{
    using RSoftware.Unity.PublisherApi.Client.Models.Internal;
    internal class DownloadsResponse : DataResponse
    {
        [JsonProperty("result")]
        public PackageLink[] Links { get; set; }
    }
}
