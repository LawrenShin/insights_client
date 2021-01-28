using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CountryData
{
    public partial class CountryEconomicPower
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double GDP { get; set; }
        public double GDPWorld { get; set; }
        public double GDPPerCapita { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
