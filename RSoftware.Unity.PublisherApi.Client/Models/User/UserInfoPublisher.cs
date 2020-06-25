
using Newtonsoft.Json;

namespace RSoftware.Unity.PublisherApi.Client.Models.User
{
    public partial class UserInfoPublisher
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
