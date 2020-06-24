
namespace RSoftware.Unity.PublisherApi.Client.Models.Sales
{
    using RSoftware.Unity.PublisherApi.Client.Misc;
    using System;

    public partial class SalesPackageInfo
    {
        private readonly string[] _data;

        public string PackageName => _data[0];

        public string RawPrice => _data[1];

        public float Price => Utility.ParseCurrency(RawPrice);

        public int Quantity => int.TryParse(_data[2], out var result) ? result : 0;

        public int Refunds => int.TryParse(_data[3], out var result) ? result : 0;

        public int Chargebacks => int.TryParse(_data[4], out var result) ? result : 0;

        public string RawGross => _data[5];

        public float Gross => Utility.ParseCurrency(RawGross);

        public DateTimeOffset FirstPurchase => Utility.ParseDt(_data[6]);

        public DateTimeOffset LastPurchase => Utility.ParseDt(_data[7]);

        public string ShortUrl { get; }

        public string RawNet { get; }

        public float Net => Utility.ParseCurrency(RawNet);

        public SalesPackageInfo(string[] data, string shortUrl, string net)
        {
            _data = data;
            ShortUrl = shortUrl;
            RawNet = net;
        }
    }
}
