
namespace RSoftware.Unity.PublisherApi.Client.Models.Publisher
{
    using Newtonsoft.Json;
    public partial class Category
    {
        [JsonProperty("label_english")]
        public string LabelEnglish { get; set; }

        [JsonProperty("slug_v2")]
        public string SlugV2 { get; set; }

        [JsonProperty("multiple")]
        public string Multiple { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
