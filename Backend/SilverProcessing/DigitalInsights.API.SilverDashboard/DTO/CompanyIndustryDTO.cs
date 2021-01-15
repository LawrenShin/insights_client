using DigitalInsights.DB.Silver.Entities;
using Newtonsoft.Json;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    internal class CompanyIndustryDTO
    {
        public CompanyIndustryDTO()
        {

        }
        public CompanyIndustryDTO(CompanyIndustry source)
        {
            if (source != null)
            {
                Industry = (int)source.Industry;
                IndustryCode = source.IndustryCode.HasValue ? (int?)source.IndustryCode.Value : null;
                PrimarySecondary = source.PrimarySecondary;
            }
        }

        [JsonProperty("industry")]
        public int? Industry { get; set; }
        [JsonProperty("industryCode")]
        public int? IndustryCode { get; set; }
        [JsonProperty("primaryOrSecondary")]
        public char? PrimarySecondary { get; set; }
    }
}