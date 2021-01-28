using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
{
    public partial class CompanyGenderMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public double? GenderMale { get; set; }
        public double? GenderOther { get; set; }
        public double? GenderRatioSenior { get; set; }
        public double? GenderRatioMiddle { get; set; }
        public double? GenderRatioAll { get; set; }
        public double? GenderPayGap { get; set; }
        public double? GenderRatioBoard { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
