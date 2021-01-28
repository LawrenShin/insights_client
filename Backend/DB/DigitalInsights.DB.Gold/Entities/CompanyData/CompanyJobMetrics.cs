using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
{
    public partial class CompanyJobMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? TotalHours { get; set; }
        public double? JobTenureAverage { get; set; }
        public double? AverageSalary { get; set; }
        public double? MedianSalary { get; set; }
        public double? EmployTurnoverTotal { get; set; }
        public double? EmployTurnoverVoluntary { get; set; }
        public double? EmployTurnoverFired { get; set; }
        public bool? EmployTraining { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
