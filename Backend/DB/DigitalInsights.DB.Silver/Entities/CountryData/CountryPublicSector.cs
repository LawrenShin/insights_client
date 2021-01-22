using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CountryData
{
    public partial class CountryPublicSector
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double HumanCapital { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
