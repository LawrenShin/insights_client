using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CountryData
{
    public partial class CountryIndustry
    {
        public int Id { get; set; }
        public Common.Enums.Industry Industry { get; set; }
        public int? CountryId { get; set; }
        public int Employees { get; set; }
        public double AveragePay { get; set; }
        public double Retention { get; set; }
        public double FlexibleHours { get; set; }
        public double EducationSpend { get; set; }
        public double Race { get; set; }
        public double Age { get; set; }
        public double Harassment { get; set; }
        public double Gender { get; set; }
        public double Maternity { get; set; }
        public double Paternity { get; set; }
        public double LBGT { get; set; }
        public double Disabled { get; set; }
        public double InjuriesNonFatal { get; set; }
        public double InjuriesFatal { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
