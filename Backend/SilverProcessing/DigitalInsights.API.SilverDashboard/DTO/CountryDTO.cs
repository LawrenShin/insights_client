using Newtonsoft.Json;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CountryDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isoCode")]
        public string ISOCode { get; set; }
    }
}