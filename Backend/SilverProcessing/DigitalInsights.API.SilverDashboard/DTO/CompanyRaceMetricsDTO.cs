using DigitalInsights.DB.Silver.Entities.CompanyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyRaceMetricsDTO
    {
        public CompanyRaceMetricsDTO()
        {

        }
        public CompanyRaceMetricsDTO(CompanyRaceMetrics source)
        {
            RaceArab = source.RaceArab;
            RaceAsian = source.RaceAsian;
            RaceBlack = source.RaceBlack;
            RaceCaucasian = source.RaceCaucasian;
            RaceHispanic = source.RaceHispanic;
            RaceIndigenous = source.RaceIndigenous;
            RaceRatioAll = source.RaceRatioAll;
            RaceRatioBoard = source.RaceRatioBoard;
            RaceRatioExececutive = source.RaceRatioExececutive;
            RaceRatioMiddle = source.RaceRatioMiddle;
            RaceRatioSenior = source.RaceRatioSenior;
        }

        public double? RaceArab { get; set; }
        public double? RaceAsian { get; set; }
        public double? RaceBlack { get; set; }
        public double? RaceCaucasian { get; set; }
        public double? RaceHispanic { get; set; }
        public double? RaceIndigenous { get; set; }
        public double? RaceRatioAll { get; set; }
        public double? RaceRatioBoard { get; set; }
        public double? RaceRatioExececutive { get; set; }
        public double? RaceRatioMiddle { get; set; }
        public double? RaceRatioSenior { get; set; }
    }
}
