using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CountryData
{
    public partial class CountryPrivateSectorAndTrade
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double CostOfBusiness { get; set; }
        public double FirmsBribery { get; set; }
        public double EaseOfBusiness { get; set; }
        public double FirmsTraining { get; set; }
        public double StartupBusiness { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
