
namespace RSoftware.Unity.PublisherApi.Client.Models.Api
{
    using System;
    using Newtonsoft.Json;

    public partial class Invoice
    {
        [JsonProperty("price_exvat")]
        public string PriceExvat { get; set; }

        [JsonProperty("downloaded")]
        public string Downloaded { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("other_license")]
        public string OtherLicense { get; set; }

        [JsonProperty("package")]
        public string Package { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("refunded")]
        public string Refunded { get; set; }

        [JsonProperty("invoice")]
        public int Id { get; set; }
    }
}
