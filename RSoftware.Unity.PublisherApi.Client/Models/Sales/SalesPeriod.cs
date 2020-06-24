using RSoftware.Unity.PublisherApi.Client.Misc;

namespace RSoftware.Unity.PublisherApi.Client.Models.Sales
{
    using Newtonsoft.Json;
    using System;

    public partial class SalesPeriod
    {
        [JsonProperty("value")]
        public string RawValue { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public DateTimeOffset Value => Utility.ParseDt(RawValue, "yyyyMM");
    }
}
