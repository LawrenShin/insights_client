using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CountryData
{
    public partial class CountryGender
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public double MalePop { get; set; }
        public double FemalePop { get; set; }
        public double WomenEdu { get; set; }
        public double FemaleWorkForce { get; set; }
        public double FemaleWorkForcePercent { get; set; }
        public double FemaleWorkForcePercentPop { get; set; }
        public double MaterintyLeave { get; set; }
        public double PaternityLeave { get; set; }
        public double GenderWorkGap { get; set; }
        public double GenderHealthGap { get; set; }
        public double GenderEduGap { get; set; }
        public double GenderPolGap { get; set; }
        public double IncomeGap { get; set; }
        public double WomenViolence { get; set; }
        public double FemaleParliamentShare { get; set; }
        public double FemaleMinisterShare { get; set; }
        public double FemalePromotionPolicy { get; set; }
        public double LifeMale { get; set; }
        public double LifeFemale { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
