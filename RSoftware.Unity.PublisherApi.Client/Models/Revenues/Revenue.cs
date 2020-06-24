using RSoftware.Unity.PublisherApi.Client.Misc;
using System;

namespace RSoftware.Unity.PublisherApi.Client.Models.Revenues
{
    public partial class Revenue
    {
        private readonly string[] _data;

        public DateTimeOffset Date => Utility.ParseDt(_data[0]);

        public string Description => _data[1];

        public string RawDebet => _data[2];
        public float Debet => Utility.ParseCurrency(RawDebet);

        public string RawCredit => _data[3];
        public float Credit => Utility.ParseCurrency(RawCredit);

        public string RawBalance => _data[4];
        public float Balance => Utility.ParseCurrency(RawBalance);

        public RevenueType Type { get; private set; }

        public Revenue(string[] data)
        {
            _data = data;
        }

        private void UpdateType(string type)
        {
            type = type.ToLowerInvariant();

            if (type.Contains("revenue"))
            {
                Type = RevenueType.Revenue;
                return;
            }

            if (type.Contains("payout"))
            {
                Type = RevenueType.Payout;
                return;
            }

            Type = RevenueType.Unknown;
        }
    }
}
