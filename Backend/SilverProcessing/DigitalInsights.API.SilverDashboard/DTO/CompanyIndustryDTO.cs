using DigitalInsights.DB.Silver.Entities;
using DigitalInsights.DB.Silver.Entities.CompanyData;
using Newtonsoft.Json;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyIndustryDTO
    {
        public CompanyIndustryDTO()
        {

        }
        public CompanyIndustryDTO(CompanyIndustry source)
        {
            if (source != null)
            {
                Industry = (int)source.Industry;
                IndustryCode = (int)source.IndustryCode;
                IsPrimary = source.IsPrimary;
                TradeDescription = source.TradeDescription;
            }
        }

        public int? Industry { get; set; }
        public int? IndustryCodeType { get; set; }
        public int? IndustryCode { get; set; }
        public bool? IsPrimary { get; set; }
        public string TradeDescription { get; set; }
    }
}