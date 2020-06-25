
namespace RSoftware.Unity.PublisherApi.Client.Models.Packages
{
    using Newtonsoft.Json;

    public partial class PackagesInfo
    {
        [JsonProperty("packages")]
        public Package[] Packages { get; set; }

        [JsonProperty("publisher_name")]
        public string PublisherName { get; set; }

        [JsonProperty("terms_current")]
        public int TermsCurrent { get; set; }

        [JsonProperty("terms_accepted")]
        public int TermsAccepted { get; set; }

        [JsonProperty("publisher_id")]
        public long PublisherId { get; set; }
    }
}
