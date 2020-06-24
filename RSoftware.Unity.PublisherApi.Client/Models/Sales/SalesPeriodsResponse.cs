namespace RSoftware.Unity.PublisherApi.Client.Models.Sales
{
    using Newtonsoft.Json;

    public partial class SalesPeriodsResponse
    {
        [JsonProperty("periods")]
        public SalesPeriod[] Periods { get; set; }
    }

}
