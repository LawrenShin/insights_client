using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
{
    public partial class CompanyRaceMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public double? RaceBlack { get; set; }
        public double? RaceAsian { get; set; }
        public double? RaceHispanic { get; set; }
        public double? RaceArab { get; set; }
        public double? RaceCaucasian { get; set; }
        public double? RaceIndigenous { get; set; }
        public double? RaceRatioExececutive { get; set; }
        public double? RaceRatioBoard { get; set; }
        public double? RaceRatioSenior { get; set; }
        public double? RaceRatioMiddle { get; set; }
        public double? RaceRatioAll { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
