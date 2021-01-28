using DigitalInsights.DB.Silver.Entities.CompanyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyHealthMetricsDTO
    {
        public CompanyHealthMetricsDTO()
        {

        }
        public CompanyHealthMetricsDTO(CompanyHealthMetrics source)
        {
            AgeAverage = source.AgeAverage;
            Fatalities = source.Fatalities;
            HealthTRI = source.HealthTRI;
            HealthTRIR = source.HealthTRIR;
            SickAbsence = source.SickAbsence;
        }

        public double? AgeAverage { get; set; }
        public int? Fatalities { get; set; }
        public int? HealthTRI { get; set; }
        public int? HealthTRIR { get; set; }
        public double? SickAbsence { get; set; }
    }
}
