using DigitalInsights.DB.Silver.Entities.CompanyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyJobMetricsDTO
    {
        public CompanyJobMetricsDTO()
        {

        }
        public CompanyJobMetricsDTO(CompanyJobMetrics source)
        {
            AverageSalary = source.AverageSalary;
            EmployTraining = source.EmployTraining;
            EmployTurnoverFired = source.EmployTurnoverFired;
            EmployTurnoverTotal = source.EmployTurnoverTotal;
            EmployTurnoverVoluntary = source.EmployTurnoverVoluntary;
            JobTenureAverage = source.JobTenureAverage;
            MedianSalary = source.MedianSalary;
            TotalHours = source.TotalHours;
        }

        public double? AverageSalary { get; set; }
        public bool? EmployTraining { get; set; }
        public double? EmployTurnoverFired { get; set; }
        public double? EmployTurnoverTotal { get; set; }
        public double? EmployTurnoverVoluntary { get; set; }
        public double? JobTenureAverage { get; set; }
        public double? MedianSalary { get; set; }
        public int? TotalHours { get; set; }
    }
}
