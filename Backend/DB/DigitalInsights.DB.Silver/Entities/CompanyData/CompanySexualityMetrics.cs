using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CompanyData
{
    public partial class CompanySexualityMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public bool? NonDiscriminationSexuality { get; set; }
        public bool? SupportDifferentSexuality { get; set; }
        public bool? LBGTQForum { get; set; }
        public bool? SexualityData { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
