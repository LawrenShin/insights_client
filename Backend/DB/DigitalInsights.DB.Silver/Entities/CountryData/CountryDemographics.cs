using System;
using System.Collections.Generic;

namespace DigitalInsights.DB.Silver.Entities.CountryData
{
    public partial class CountryDemographics
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public double Population { get; set; }
        public double ImmigrantPop { get; set; }
        public double ImmigrantPercent { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
