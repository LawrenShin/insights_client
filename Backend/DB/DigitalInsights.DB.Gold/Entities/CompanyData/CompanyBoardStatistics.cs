using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
{
    public partial class CompanyBoardStatistics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int MembersNumber { get; set; }
        public double? SalaryAverage { get; set; }
        public double? SalaryMean { get; set; }
        public double? ArabPercentage { get; set; }
        public double? HispanicPercentage { get; set; }
        public double? BlackPercentage { get; set; }
        public double? AsianPercentage { get; set; }
        public double? CaucasianPercentage { get; set; }
        public double? IndigenousPercentage { get; set; }
        public double? AverageAge { get; set; }
        public double? AverageEducationLength { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
