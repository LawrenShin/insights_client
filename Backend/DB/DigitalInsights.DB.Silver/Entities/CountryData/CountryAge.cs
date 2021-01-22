using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CountryData
{
    public partial class CountryAge
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double AgeDistribution19 { get; set; }
        public double AgeDistribution39 { get; set; }
        public double AgeDistribution59 { get; set; }
        public double AgeDistribution79 { get; set; }
        public double AgeDistributionx { get; set; }
        public double AgeAverage18 { get; set; }
        public double AgeParliament { get; set; }
        public double AgeMinisters { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
