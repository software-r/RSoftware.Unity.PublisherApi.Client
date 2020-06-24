using Newtonsoft.Json;

namespace RSoftware.Unity.PublisherApi.Client.Models.User
{
    public partial class Balance
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("amount_text")]
        public string AmountText { get; set; }
    }

}
