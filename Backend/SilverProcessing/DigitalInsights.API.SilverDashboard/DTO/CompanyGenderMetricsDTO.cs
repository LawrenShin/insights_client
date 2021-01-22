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
        public CompanyGenderMetricsDTO(CompanyGenderMetrics source)
        {
            GenderMale = source.GenderMale;
            GenderOther =source.GenderOther;
            GenderPayGap = source.GenderPayGap;
            GenderRatioAll = source.GenderRatioAll;
            GenderRatioBoard = source.GenderRatioBoard;
            GenderRatioMiddle = source.GenderRatioMiddle;
            GenderRatioSenior = source.GenderRatioSenior;
        }

        public double? GenderMale { get; private set; }
        public double? GenderOther { get; private set; }
        public double? GenderPayGap { get; private set; }
        public double? GenderRatioAll { get; private set; }
        public double? GenderRatioBoard { get; private set; }
        public double? GenderRatioMiddle { get; private set; }
        public double? GenderRatioSenior { get; private set; }
    }
}
