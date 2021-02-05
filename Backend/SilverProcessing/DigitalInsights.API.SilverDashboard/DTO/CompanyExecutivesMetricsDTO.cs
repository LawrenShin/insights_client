using DigitalInsights.DB.Silver.Entities.CompanyData;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyExecutiveStatisticsDTO
    {
        public CompanyExecutiveStatisticsDTO()
        {
        }
        public CompanyExecutiveStatisticsDTO(CompanyExecutiveStatistics source)
        {
            ArabPercentage = source.ArabPercentage;
            AsianPercentage = source.AsianPercentage;
            AverageAge = source.AverageAge;
            AverageEducationLength = source.AverageEducationLength;
            BlackPercentage = source.BlackPercentage;
            CaucasianPercentage = source.CaucasianPercentage;
            Height = source.Height;
            HispanicPercentage = source.HispanicPercentage;
            IndigenousPercentage = source.IndigenousPercentage;
            MembersNumber = source.MembersNumber;
            SalaryAverage = source.SalaryAverage;
            SalaryMean = source.SalaryMean;
            Weight = source.Weight;
        }

        public double? ArabPercentage { get; set; }
        public double? AsianPercentage { get; set; }
        public double? AverageAge { get; set; }
        public double? AverageEducationLength { get; set; }
        public double? BlackPercentage { get; set; }
        public double? CaucasianPercentage { get; set; }
        public double? Height { get; set; }
        public double? HispanicPercentage { get; set; }
        public double? IndigenousPercentage { get; set; }
        public int MembersNumber { get; set; }
        public double? SalaryAverage { get; set; }
        public double? SalaryMean { get; set; }
        public double? Weight { get; set; }
    }
}
