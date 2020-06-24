
namespace RSoftware.Unity.PublisherApi.Client.Models.Sales
{
    using Newtonsoft.Json;

    internal partial class SalesResponse
    {
        [JsonProperty("aaData")]
        public string[][] Payload { get; set; }

        [JsonProperty("result")]
        public SalesPackageIncome[] PackageIncomes { get; set; }
    }
}
