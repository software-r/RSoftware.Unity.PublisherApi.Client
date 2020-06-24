namespace RSoftware.Unity.PublisherApi.Client.Models.Publisher
{
    using Newtonsoft.Json;

    public partial class LatestInfo
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("pubdate")]
        public string PublishDate { get; set; }

        [JsonProperty("package_version_id")]
        public long PackageVersionId { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("publisher")]
        public Publisher Publisher { get; set; }

        [JsonProperty("list")]
        public LatestInfoList[] Lists { get; set; }

        [JsonProperty("link")]
        public Link Link { get; set; }

        [JsonProperty("flags")]
        public Flags Flags { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("keyimage")]
        public LatestInfoKeyImage Images { get; set; }

        [JsonProperty("license")]
        public long License { get; set; }

        [JsonProperty("title_english")]
        public string TitleEnglish { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
