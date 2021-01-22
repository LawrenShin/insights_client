using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CountryData
{
    public partial class CountryDemographics
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double Population { get; set; }
        public double ImmigrantPopulation { get; set; }
        public double ImmigrantPercentage { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
