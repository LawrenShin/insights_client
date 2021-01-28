using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CountryData
{
    public partial class CountryDisability
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double DisabilityDiscriminationLaw { get; set; }
        public double Disabled { get; set; }
        public double Overweight { get; set; }
        public double HealthFundingGDP { get; set; }
        public double HealthType { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
