using Newtonsoft.Json;

namespace DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.API
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class FileDescription
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("record_count")]
        public int RecordCount { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("size_human_readable")]
        public string SizeHumanReadable { get; set; }

        [JsonProperty("delta_type")]
        public string DeltaType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
