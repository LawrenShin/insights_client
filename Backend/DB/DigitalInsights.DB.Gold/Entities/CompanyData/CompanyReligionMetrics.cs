using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
{
    public partial class CompanyReligionMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public double? MuslimShare { get; set; }
        public double? ChristianShare { get; set; }
        public double? HinduShare { get; set; }
        public double? BuddhismShare { get; set; }
        public double? JudaismShare { get; set; }
        public double? OtherShare { get; set; }
        public bool? NonDiscriminationReligion { get; set; }
        public bool? HolidayReligion { get; set; }
        public bool? PrayerRoom { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
