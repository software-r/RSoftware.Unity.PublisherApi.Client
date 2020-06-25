namespace RSoftware.Unity.PublisherApi.Client.Models.Packages
{
    using Newtonsoft.Json;

    public partial class Package
    {
        [JsonProperty("management_flags")]
        public string ManagementFlags { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("versions")]
        public PackageVersion[] Versions { get; set; }

        [JsonProperty("category_id")]
        public int CategoryId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("short_url")]
        public string ShortUrl { get; set; }

        [JsonProperty("average_rating")]
        public string AverageRating { get; set; }

        [JsonProperty("count_ratings")]
        public string CountRatings { get; set; }
    }
}
