using Newtonsoft.Json;

namespace DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.API
{
    public class Lei
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("publish_date")]
        public string PublishDate { get; set; }

        [JsonProperty("full_file")]
        public FileBundle FullFile { get; set; }

        [JsonProperty("delta_files")]
        public FileBundle DeltaFiles { get; set; }
    }
}
