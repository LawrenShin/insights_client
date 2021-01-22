using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CompanyData
{
    public partial class CompanyFamilyMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public bool? ParentalLeave { get; set; }
        public int? ParentalTime { get; set; }
        public bool? ParentalGender { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
