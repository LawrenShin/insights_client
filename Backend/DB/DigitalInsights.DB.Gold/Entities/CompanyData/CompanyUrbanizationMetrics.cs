using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
{
    public partial class CompanyUrbanizationMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? UrbanSites { get; set; }
        public int? RuralSites { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
