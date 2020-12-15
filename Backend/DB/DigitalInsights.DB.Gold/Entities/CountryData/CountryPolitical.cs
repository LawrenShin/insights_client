using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CountryData
{
    public partial class CountryPolitical
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public double Democracy { get; set; }
        public double Corruption { get; set; }
        public double FreedomSpeech { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
