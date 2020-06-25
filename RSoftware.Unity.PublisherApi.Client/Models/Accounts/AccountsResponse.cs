

namespace RSoftware.Unity.PublisherApi.Client.Models.Accounts
{
    using Newtonsoft.Json;

    internal class AccountsResponse
    {

        [JsonProperty("aaData")]
        public string[][] Payload { get; set; }
    }
}
