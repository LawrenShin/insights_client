using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CompanyData
{
    public partial class CompanyKeyFinancialsMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public double? OperatingRevenue { get; set; }
        public int? TotalAssets { get; set; }
        public int? Employees { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
