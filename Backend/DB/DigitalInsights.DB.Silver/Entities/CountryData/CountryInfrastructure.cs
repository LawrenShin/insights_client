using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CountryData
{
    public partial class CountryInfrastructure
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double InternetUse { get; set; }
        public double CellularSubscriptions { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
