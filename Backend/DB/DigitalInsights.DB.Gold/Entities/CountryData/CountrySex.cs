using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CountryData
{
    public partial class CountrySex
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public double SameMarriage { get; set; }
        public double HomosexualTolerance { get; set; }
        public double HomosexualPop { get; set; }
        public double SameAdopt { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
