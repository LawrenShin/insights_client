using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CountryData
{
    public partial class CountryRace
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public double Black { get; set; }
        public double Asian { get; set; }
        public double Hispanic { get; set; }
        public double Arab { get; set; }
        public double Caucasian { get; set; }
        public double Indegineous { get; set; }
        public double DiscriminationLaw { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
