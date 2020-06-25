

namespace RSoftware.Unity.PublisherApi.Client.Models.Vouchers
{
    using RSoftware.Unity.PublisherApi.Client.Misc;
    using System;

    public class Voucher
    {
        private readonly string[] _data;

        public string Code => _data[0];
        public string PackageName => _data[1];
        public string IssuedBy => _data[2];
        public DateTimeOffset IssuedDate => Utility.ParseDt(_data[3]);
        public string InvoiceId => _data[4];
        public DateTimeOffset ReedemedDate => Utility.ParseDt(_data[5]);

        public Voucher(string[] data)
        {
            _data = data;
        }
    }
}
