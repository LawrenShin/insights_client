using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CountryData
{
    public partial class CountryEconomy
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public double Gdp { get; set; }
        public double GdpPerCapita { get; set; }
        public double LabourForce { get; set; }
        public double GdpWorld { get; set; }
        public double LabourForcePercent { get; set; }
        public double MaleUnemploy { get; set; }
        public double FemaleUnemploy { get; set; }
        public double AvgIncome { get; set; }
        public double Poor { get; set; }
        public double EqualityLevel { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
