using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CountryData
{
    public partial class CountryEconomicEquality
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double Poor { get; set; }
        public double EqualityIndex { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
