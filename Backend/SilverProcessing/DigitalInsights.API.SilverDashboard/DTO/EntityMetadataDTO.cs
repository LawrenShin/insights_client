using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class EntityMetadataDTO
    {
        public EntityMetadataDTO()
        {
        }

        [JsonProperty("entityName")]
        public string EntityName { get; set; }

        [JsonProperty("propertyMetadata")]
        public PropertyMetadataDTO[] PropertyMetadata { get; set; }
    }
}
