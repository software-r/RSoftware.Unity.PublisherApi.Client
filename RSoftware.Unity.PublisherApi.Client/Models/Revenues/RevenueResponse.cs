
namespace RSoftware.Unity.PublisherApi.Client.Models.Revenues
{
    using Newtonsoft.Json;

    internal partial class RevenueResponse
    {
        [JsonProperty("aaData")]
        public string[][] Payload { get; set; }
    }
}
