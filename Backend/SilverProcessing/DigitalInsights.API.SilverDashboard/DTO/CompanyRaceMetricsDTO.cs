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

        public double? RaceArab { get; private set; }
        public double? RaceAsian { get; private set; }
        public double? RaceBlack { get; private set; }
        public double? RaceCaucasian { get; private set; }
        public double? RaceHispanic { get; private set; }
        public double? RaceIndigenous { get; private set; }
        public double? RaceRatioAll { get; private set; }
        public double? RaceRatioBoard { get; private set; }
        public double? RaceRatioExececutive { get; private set; }
        public double? RaceRatioMiddle { get; private set; }
        public double? RaceRatioSenior { get; private set; }
    }
}
