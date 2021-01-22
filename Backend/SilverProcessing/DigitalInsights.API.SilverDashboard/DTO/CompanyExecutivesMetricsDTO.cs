using DigitalInsights.DB.Silver.Entities.CompanyData;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyExecutiveStatisticsDTO
    {
        public CompanyExecutiveStatisticsDTO(CompanyExecutiveStatistics source)
        {
            ArabPercentage = source.ArabPercentage;
            AsianPercentage = source.AsianPercentage;
            AverageAge = source.AverageAge;
            AverageEducationLength = source.AverageEducationLength;
            BlackPercentage = source.BlackPercentage;
            CaucasianPercentage = source.CaucasianPercentage;
            FemaleRatio = source.FemaleRatio;
            Height = source.Height;
            HispanicPercentage = source.HispanicPercentage;
            IndigenousPercentage = source.IndigenousPercentage;
            MembersNumber = source.MembersNumber;
            SalaryAverage = source.SalaryAverage;
            SalaryMean = source.SalaryMean;
            Weight = source.Weight;
        }

        public double? ArabPercentage { get; private set; }
        public double? AsianPercentage { get; private set; }
        public double? AverageAge { get; private set; }
        public double? AverageEducationLength { get; private set; }
        public double? BlackPercentage { get; private set; }
        public double? CaucasianPercentage { get; private set; }
        public double? FemaleRatio { get; private set; }
        public double? Height { get; private set; }
        public double? HispanicPercentage { get; private set; }
        public double? IndigenousPercentage { get; private set; }
        public int MembersNumber { get; private set; }
        public double? SalaryAverage { get; private set; }
        public double? SalaryMean { get; private set; }
        public double? Weight { get; private set; }
    }
}
