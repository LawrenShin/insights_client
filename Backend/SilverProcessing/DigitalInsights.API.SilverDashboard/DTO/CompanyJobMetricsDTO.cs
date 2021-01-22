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

        public double? AverageSalary { get; private set; }
        public double? EmployTraining { get; private set; }
        public double? EmployTurnoverFired { get; private set; }
        public double? EmployTurnoverTotal { get; private set; }
        public double? EmployTurnoverVoluntary { get; private set; }
        public double? JobTenureAverage { get; private set; }
        public double? MedianSalary { get; private set; }
        public int? TotalHours { get; private set; }
    }
}
