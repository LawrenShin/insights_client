using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CompanyData
{
    public partial class CompanyHealthMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public double? AgeAverage { get; set; }
        public int? Fatalities { get; set; }
        public double? SickAbsence { get; set; }
        public int? HealthTRI { get; set; }
        public int? HealthTRIR { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
