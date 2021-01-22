using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CompanyData
{
    public partial class CompanyIndustry
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Common.Enums.Industry Industry { get; set; }
        public Common.Enums.IndustryCode IndustryCode { get; set; }
        public string TradeDescription { get; set; }
        public bool? IsPrimary { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
