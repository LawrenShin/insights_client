using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.API
{

    public class ServiceResponse
    {
        [JsonProperty("data")]
        public List<Datum> Data { get; set; }
    }
}
