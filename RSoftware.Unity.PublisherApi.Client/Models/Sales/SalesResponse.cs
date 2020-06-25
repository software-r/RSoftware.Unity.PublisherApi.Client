

namespace RSoftware.Unity.PublisherApi.Client.Models.Sales
{
    using Newtonsoft.Json;
    using RSoftware.Unity.PublisherApi.Client.Models.Internal;

    internal class SalesResponse : DataResponse
    {
        [JsonProperty("result")]
        public SalesPackageIncome[] PackageIncomes { get; set; }
    }
}
