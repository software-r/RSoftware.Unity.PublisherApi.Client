namespace RSoftware.Unity.PublisherApi.Client.Models.Invoices
{
    using Misc;
    using System;

    public partial class Invoice
    {
        private readonly string[] _data;

        public string Id => _data[0];
        public string Package => _data[1];
        public int Quantity => int.TryParse(_data[2], out var result) ? result : 0;
        public string RawPrice => _data[3];
        public float TotalPrice => Utility.ParseFloat(RawPrice);
        public DateTimeOffset Date => Utility.ParseDt(_data[4]);
        public InvoiceStatus Status { get; private set; }
        public bool IsRefunded => Status == InvoiceStatus.Refunded && Status == InvoiceStatus.ChargedBack;

        public Invoice(string[] data)
        {
            _data = data;

            UpdateStatus(_data[5]);
        }

        private void UpdateStatus(string status)
        {
            status = status.ToLowerInvariant();

            if (status.Contains("not downloaded"))
            {
                Status = InvoiceStatus.NotDownloaded;
                return;
            }

            if (status.Contains("downloaded"))
            {
                Status = InvoiceStatus.Downloaded;
                return;
            }

            if (status.Contains("license"))
            {
                Status = InvoiceStatus.AnotherLicense;
                return;
            }


            if (status.Contains("refunded"))
            {
                Status = InvoiceStatus.Refunded;
                return;
            }


            if (status.Contains("charge"))
            {
                Status = InvoiceStatus.ChargedBack;
                return;
            }

            Status = InvoiceStatus.Unknown;
        }
    }
}
