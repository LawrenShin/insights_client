using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CountryData
{
    public partial class CountryLaborForce
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double LaborForce { get; set; }
        public double LaborForcePercentage { get; set; }
        public double MaleUnemployment { get; set; }
        public double FemaleUnemployment { get; set; }
        public double AverageIncome { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
