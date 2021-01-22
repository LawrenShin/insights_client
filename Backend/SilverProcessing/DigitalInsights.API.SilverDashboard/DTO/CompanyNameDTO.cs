using DigitalInsights.DB.Silver.Entities;
using DigitalInsights.DB.Silver.Entities.CompanyData;
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

        public string Name { get; private set; }
        public string NameType { get; private set; }
    }
}