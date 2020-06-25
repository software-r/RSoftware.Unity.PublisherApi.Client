
namespace RSoftware.Unity.PublisherApi.Client.Models.Internal
{
    using Newtonsoft.Json;

    internal abstract class DataResponse
    {
        [JsonProperty("aaData")]
        public string[][] Payload { get; set; }
    }
}
