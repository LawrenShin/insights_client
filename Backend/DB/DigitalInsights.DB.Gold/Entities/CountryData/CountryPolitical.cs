using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CountryData
{
    public partial class CountryPolitical
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double DemocracyIndex { get; set; }
        public double CorruptionIndex { get; set; }
        public double FreeSpeechIndex { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
