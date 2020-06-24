namespace RSoftware.Unity.PublisherApi.Client.Models.Sales
{
    using Newtonsoft.Json;

    public partial class SalesPackageIncome
    {
        [JsonProperty("short_url")]
        public string ShortUrl { get; set; }

        [JsonProperty("net", NullValueHandling = NullValueHandling.Ignore)]
        public string RawNet { get; set; }
    }
}
