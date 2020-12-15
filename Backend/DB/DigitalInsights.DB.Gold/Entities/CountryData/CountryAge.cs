using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CountryData
{
    public partial class CountryAge
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public double Dis19 { get; set; }
        public double Dis39 { get; set; }
        public double Dis59 { get; set; }
        public double Dis70 { get; set; }
        public double Disx { get; set; }
        public double Avg18 { get; set; }
        public double Parliament { get; set; }
        public double Ministers { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
