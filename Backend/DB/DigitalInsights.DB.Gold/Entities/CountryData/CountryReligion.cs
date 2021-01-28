﻿using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CountryData
{
    public partial class CountryReligion
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double Muslim { get; set; }
        public double Christian { get; set; }
        public double Hindu { get; set; }
        public double Buddishm { get; set; }
        public double Judaism { get; set; }
        public double Other { get; set; }
        public bool StateReligion { get; set; }
        public bool ReligionFreedom { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
