using System.Text.Json.Serialization;

namespace UC1.Extensions
{
    public class Country
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("capital")]
        public string? Capital { get; set; }
        [JsonPropertyName("region")]
        public string? Region { get; set; }
        [JsonPropertyName("subregion")]
        public string? SubRegion { get; set; }
        [JsonPropertyName("population")]
        public long? Population { get; set; }
    }
}
