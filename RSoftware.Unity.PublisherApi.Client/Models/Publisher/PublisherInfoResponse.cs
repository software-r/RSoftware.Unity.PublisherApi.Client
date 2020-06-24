namespace RSoftware.Unity.PublisherApi.Client.Models.Publisher
{
    using Newtonsoft.Json;

    internal partial class PublisherInfoResponse
    {
        [JsonProperty("overview")]
        public PublisherInfo Overview { get; set; }
    }
}
