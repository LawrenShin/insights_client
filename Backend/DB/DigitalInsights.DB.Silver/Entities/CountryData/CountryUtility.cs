using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CountryData
{
    public partial class CountryUtility
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double SlumsPopulation { get; set; }
        public double AccessToElectricity { get; set; }
        public double AccessToDrinkingWater { get; set; }
        public double AccessToSanitation { get; set; }
        public double AccessToHandWashing { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
