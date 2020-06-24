using System;
using Newtonsoft.Json;

namespace RSoftware.Unity.PublisherApi.Client.Models.User
{
    public partial class UserInfoKeyImage
    {
        [JsonProperty("large")]
        public Uri Large { get; set; }

        [JsonProperty("small")]
        public Uri Small { get; set; }

        [JsonProperty("icon")]
        public Uri Icon { get; set; }

        [JsonProperty("icon24")]
        public Uri Icon24 { get; set; }

        [JsonProperty("medium")]
        public Uri Medium { get; set; }
    }
}
