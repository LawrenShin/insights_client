using DigitalInsights.DB.Silver.Entities;
using System.Linq;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyDTO
    {
        public CompanyDTO()
        {
        }

        public CompanyDTO(Company source)
        {
            Addresses = source.CompanyAddresses.Select(x => new AddressDTO(x)).ToArray();
            CompanyBoardStatistics = source.CompanyBoardStatistics.Select(x => new CompanyBoardStatisticsDTO(x)).ToArray();
            CompanyCountries = source.CompanyCountries.Select(x => new CompanyCountryDTO(x)).ToArray();
            CompanyDIMetrics = source.CompanyDIMetrics.Select(x => new CompanyDIMetricsDTO(x)).ToArray();
            CompanyExecutiveStatistics = source.CompanyExecutiveStatistics.Select(x => new CompanyExecutiveStatisticsDTO(x)).ToArray();
            CompanyGenderMetrics = source.CompanyGenderMetrics.Select(x => new CompanyGenderMetricsDTO(x)).ToArray();
            CompanyHealthMetrics = source.CompanyHealthMetrics.Select(x => new CompanyHealthMetricsDTO(x)).ToArray();
            CompanyIndustries = source.CompanyIndustries.Select(x => new CompanyIndustryDTO(x)).ToArray();
            CompanyJobMetrics = source.CompanyJobMetrics.Select(x => new CompanyJobMetricsDTO(x)).ToArray();
            CompanyKeyFinancialsMetrics = source.CompanyKeyFinancialsMetrics.Select(x => new CompanyKeyFinancialsMetricsDTO(x)).ToArray();
            CompanyNames = source.CompanyNames.Select(x => new CompanyNameDTO(x)).ToArray();
            CompanyRaceMetrics = source.CompanyRaceMetrics.Select(x => new CompanyRaceMetricsDTO(x)).ToArray();
            Roles = source.Roles.Select(x => new RoleDTO(x)).ToArray();
            Id = source.Id;
            LegalName = source.LegalName;
            Lei = source.LEI;
        }

        public int Id { get; private set; }
        public string LegalName { get; private set; }
        public string Lei { get; private set; }
        public AddressDTO[] Addresses { get; private set; }
        public CompanyBoardStatisticsDTO[] CompanyBoardStatistics { get; set; }
        public CompanyCountryDTO[] CompanyCountries { get; }
        public CompanyDIMetricsDTO[] CompanyDIMetrics { get; }
        public CompanyExecutiveStatisticsDTO[] CompanyExecutiveStatistics { get; }
        public CompanyGenderMetricsDTO[] CompanyGenderMetrics { get; }
        public CompanyHealthMetricsDTO[] CompanyHealthMetrics { get; }
        public CompanyIndustryDTO[] CompanyIndustries { get; }
        public CompanyJobMetricsDTO[] CompanyJobMetrics { get; }
        public CompanyKeyFinancialsMetricsDTO[] CompanyKeyFinancialsMetrics { get; set; }
        public CompanyNameDTO[] CompanyNames { get; set; }
        public CompanyRaceMetricsDTO[] CompanyRaceMetrics { get; set; }
        public RoleDTO[] Roles { get; set; }
    }
}
