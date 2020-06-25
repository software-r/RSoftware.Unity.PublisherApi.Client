namespace RSoftware.Unity.PublisherApi.Client.Models.Packages
{
    using Newtonsoft.Json;
    using RSoftware.Unity.PublisherApi.Client.Misc;
    using System;

    public partial class PackageVersion
    {
        [JsonProperty("status")]
        public string RawStatus { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("package_id")]
        public int PackageId { get; set; }

        [JsonProperty("modified")]
        public DateTimeOffset Modified { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("published")]
        public DateTimeOffset Published { get; set; }

        [JsonProperty("version_name")]
        public string VersionName { get; set; }

        [JsonProperty("category_id")]
        public int CategoryId { get; set; }

        [JsonProperty("package_version_id")]
        public int PackageVersionId { get; set; }

        [JsonProperty("publishnotes")]
        public string Notes { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("price")]
        public string RawPrice { get; set; }

        [JsonIgnore]
        public float Price => Utility.ParseFloat(RawPrice);

        [JsonIgnore]
        public VersionStatus Status => Enum.TryParse(RawStatus, true, out VersionStatus result) ? result : VersionStatus.Unknown;
    }
}
