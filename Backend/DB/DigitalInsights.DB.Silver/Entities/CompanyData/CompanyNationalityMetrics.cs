using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CompanyData
{
    public partial class CompanyNationalityMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? NationalNumberOpeRation { get; set; }
        public int? NationalDifferent { get; set; }
        public string NationalTopFive { get; set; }
        public bool? CultureERG { get; set; }
        public bool? SupportLanguages { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
