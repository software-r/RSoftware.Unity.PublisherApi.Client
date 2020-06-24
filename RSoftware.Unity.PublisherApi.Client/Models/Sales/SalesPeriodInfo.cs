
using System.Linq;

namespace RSoftware.Unity.PublisherApi.Client.Models.Sales
{
    using System.Collections.Generic;

    public partial class SalesPeriodInfo
    {
        public List<SalesPackageInfo> Packages { get; }

        public float PayoutCut { get; }

        public float Gross => Packages.Sum(p => p.Gross);
        public float Net => Packages.Sum(p => p.Net);

        public SalesPeriodInfo(SalesPackageInfo[] packages, float payoutCut)
        {
            Packages = new List<SalesPackageInfo>(packages);
            PayoutCut = payoutCut;
        }

        protected SalesPeriodInfo()
        {
        }
    }
}
