
using Newtonsoft.Json;

namespace RSoftware.Unity.PublisherApi.Client.Models.User
{
    public partial class UserInfoPublisher
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
