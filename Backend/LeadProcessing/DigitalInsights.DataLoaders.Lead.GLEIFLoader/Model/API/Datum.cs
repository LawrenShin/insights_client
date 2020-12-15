using Newtonsoft.Json;

namespace DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.API
{
    public class Datum
    {
        [JsonProperty("publish_date")]
        public string PublishDate { get; set; }

        [JsonProperty("lei2")]
        public Lei Lei2 { get; set; }
    }
}
