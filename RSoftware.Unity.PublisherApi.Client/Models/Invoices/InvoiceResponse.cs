using Newtonsoft.Json;

namespace RSoftware.Unity.PublisherApi.Client.Models.Invoices
{
    public class InvoiceResponse
    {
        [JsonProperty("aaData")]
        public string[][] Payload { get; set; }
    }
}
