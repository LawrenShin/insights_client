using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CountryData
{
    public partial class CountryDemographic
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
