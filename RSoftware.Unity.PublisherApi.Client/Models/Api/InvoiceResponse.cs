namespace RSoftware.Unity.PublisherApi.Client.Models.Api
{
    using Newtonsoft.Json;
    public class InvoiceResponse
    {
        [JsonProperty("invoices")]
        public Invoice[] Invoices { get; set; }
    }
}
