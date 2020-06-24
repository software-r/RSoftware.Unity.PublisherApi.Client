
using Newtonsoft.Json;

namespace RSoftware.Unity.PublisherApi.Client.Models.User
{
    public partial class UserInfo
    {
        [JsonProperty("downloader")]
        public bool Downloader { get; set; }

        [JsonProperty("language_code")]
        public string LanguageCode { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("v2editor_allowed")]
        public bool EditorAllowed { get; set; }

        [JsonProperty("has_accepted_latest_terms")]
        public bool HasAcceptedLatestTerms { get; set; }

        [JsonProperty("himself")]
        public bool Himself { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("publisher")]
        public UserInfoPublisher Publisher { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("publisher_id")]
        public long PublisherId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("keyimage")]
        public UserInfoKeyImage Images { get; set; }

        [JsonProperty("balance")]
        public Balance Balance { get; set; }

        [JsonProperty("editable")]
        public bool Editable { get; set; }

        [JsonProperty("v2editor_preferred")]
        public bool EditorPreferred { get; set; }

        [JsonProperty("v2_preferred")]
        public bool Preferred { get; set; }
    }
}
