using Newtonsoft.Json;

namespace DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.API
{
    public class FileBundle
    {
        [JsonProperty("csv")]
        public FileDescription Csv { get; set; }

        [JsonProperty("json")]
        public FileDescription Json { get; set; }

        [JsonProperty("xml")]
        public FileDescription Xml { get; set; }
    }
}
