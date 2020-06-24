
namespace RSoftware.Unity.PublisherApi.Client.Models.Publisher
{
    using Newtonsoft.Json;

    public partial class PublisherInfo
    {
        [JsonProperty("organization_id")]
        public string OrganizationId { get; set; }

        [JsonProperty("latest")]
        public LatestInfo Latest { get; set; }

        [JsonProperty("services")]
        public bool Services { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("keyimage")]
        public PublisherInfoKeyImage Images { get; set; }

        [JsonProperty("support_email")]
        public string SupportEmail { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rating")]
        public Rating Rating { get; set; }

        [JsonProperty("auth")]
        public string Auth { get; set; }

        [JsonProperty("payout_cut")]
        public string PayoutCut { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("short_url")]
        public string ShortUrl { get; set; }

        [JsonProperty("support_url")]
        public string SupportUrl { get; set; }
    }
}
