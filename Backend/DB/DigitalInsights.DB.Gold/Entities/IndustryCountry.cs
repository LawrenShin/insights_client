using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities
{
    public partial class IndustryCountry
    {
        public int Id { get; set; }
        public int? IndustryId { get; set; }
        public int? CountryId { get; set; }
        public int NumEmployees { get; set; }
        public double AvgPay { get; set; }
        public double RententionRate { get; set; }
        public double EducationSpend { get; set; }
        public double FlexibleHoursPledge { get; set; }
        public double DiPledge { get; set; }
        public double HarassmentPledge { get; set; }
        public double IndustryDiversity { get; set; }
        public double WomenEmployeedPercent { get; set; }
        public double MaterintyLeavePledge { get; set; }
        public double PaternityLeavePledge { get; set; }
        public double LgbtPledge { get; set; }
        public double DisabilitiesPledge { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
        public virtual Industry Industry { get; set; }
    }
}
