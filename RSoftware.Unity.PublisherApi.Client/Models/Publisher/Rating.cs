namespace RSoftware.Unity.PublisherApi.Client.Models.Publisher
{
    using Newtonsoft.Json;
    public partial class Rating
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("average")]
        public long Average { get; set; }
    }
}
