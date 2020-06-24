namespace RSoftware.Unity.PublisherApi.Client.Models.Invoices
{
    public enum InvoiceStatus
    {
        Unknown = -1,
        Downloaded = 1,
        NotDownloaded = 2,
        AnotherLicense = 3,
        Refunded = 4,
        ChargedBack = 5,
    }
}
