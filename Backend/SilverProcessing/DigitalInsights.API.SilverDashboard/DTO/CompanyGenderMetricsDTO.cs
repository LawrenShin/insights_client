using DigitalInsights.DB.Silver.Entities.CompanyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyGenderMetricsDTO
    {
        public CompanyGenderMetricsDTO()
        {

        }
        public CompanyGenderMetricsDTO(CompanyGenderMetrics source)
        {
            GenderMale = source.GenderMale;
            GenderOther =source.GenderOther;
            GenderPayGap = source.GenderPayGap;
            GenderRatioAll = source.GenderRatioAll;
            GenderRatioBoard = source.GenderRatioBoard;
            GenderRatioExecutive = source.GenderRatioExecutive;
            GenderRatioMiddle = source.GenderRatioMiddle;
            GenderRatioSenior = source.GenderRatioSenior;
        }

        public double? GenderMale { get; set; }
        public double? GenderOther { get; set; }
        public double? GenderPayGap { get; set; }
        public double? GenderRatioAll { get; set; }
        public double? GenderRatioBoard { get; set; }
        public double? GenderRatioExecutive { get; set; }
        public double? GenderRatioMiddle { get; set; }
        public double? GenderRatioSenior { get; set; }
    }
}
