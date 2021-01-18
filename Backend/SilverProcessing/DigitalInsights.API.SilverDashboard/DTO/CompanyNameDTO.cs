using DigitalInsights.DB.Silver.Entities;
using Newtonsoft.Json;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyNameDTO
    {
        public CompanyNameDTO()
        {

        }

        public CompanyNameDTO(CompanyName source)
        {
            if (source != null)
            {
                Name = source.Name;
                NameType = source.NameType;
            }
        }

        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("nameType")]
        public string NameType { get; private set; }
    }
}