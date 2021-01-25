using Newtonsoft.Json;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    [JsonConverter(typeof(UIMetadataDTOConverter))]
    public class UIMetadataDTO
    {

        public EntityMetadataDTO[] Entities { get; set; }
    }
}