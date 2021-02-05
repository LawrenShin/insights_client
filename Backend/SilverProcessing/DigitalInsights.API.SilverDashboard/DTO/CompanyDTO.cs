using DigitalInsights.DB.Silver.Entities;
using System.Linq;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyDTO
    {
        public CompanyDTO()
        {
            Addresses = new AddressDTO[0];
            BoardStatistics = new CompanyBoardStatisticsDTO();
            Countries = new CompanyCountryDTO[0];
            DiMetrics = new CompanyDIMetricsDTO();
            ExecutiveStatistics = new CompanyExecutiveStatisticsDTO();
            GenderMetrics = new CompanyGenderMetricsDTO();
            HealthMetrics = new CompanyHealthMetricsDTO();
            Industries = new CompanyIndustryDTO[0];
            JobMetrics = new CompanyJobMetricsDTO();
            KeyFinancialsMetrics = new CompanyKeyFinancialsMetricsDTO();
            Names = new CompanyNameDTO[0];
            RaceMetrics = new CompanyRaceMetricsDTO();
            Roles = new RoleDTO[0];
        }

        public CompanyDTO(Company source)
        {
            Addresses = source.CompanyAddresses.Select(x => new AddressDTO(x)).ToArray();
            BoardStatistics = new CompanyBoardStatisticsDTO(source.CompanyBoardStatistics.First());
            Countries = source.CompanyCountries.Select(x => new CompanyCountryDTO(x)).ToArray();
            DiMetrics = new CompanyDIMetricsDTO(source.CompanyDIMetrics.First());
            ExecutiveStatistics = new CompanyExecutiveStatisticsDTO(source.CompanyExecutiveStatistics.First());
            GenderMetrics = new CompanyGenderMetricsDTO(source.CompanyGenderMetrics.First());
            HealthMetrics = new CompanyHealthMetricsDTO(source.CompanyHealthMetrics.First());
            Industries = source.CompanyIndustries.Select(x => new CompanyIndustryDTO(x)).ToArray();
            JobMetrics = new CompanyJobMetricsDTO(source.CompanyJobMetrics.First());
            KeyFinancialsMetrics = new CompanyKeyFinancialsMetricsDTO(source.CompanyKeyFinancialsMetrics.First());
            Names = source.CompanyNames.Select(x => new CompanyNameDTO(x)).ToArray();
            RaceMetrics = new CompanyRaceMetricsDTO(source.CompanyRaceMetrics.First());
            Roles = source.Roles.Select(x => new RoleDTO(x)).ToArray();
            Id = source.Id;
            LegalName = source.LegalName;
            Lei = source.LEI;
        }

        public int Id { get; set; }
        public string LegalName { get; set; }
        public string Lei { get; set; }
        public AddressDTO[] Addresses { get; set; }
        public CompanyBoardStatisticsDTO BoardStatistics { get; set; }
        public CompanyCountryDTO[] Countries { get; set; }
        public CompanyDIMetricsDTO DiMetrics { get; set; }
        public CompanyExecutiveStatisticsDTO ExecutiveStatistics { get; set; }
        public CompanyGenderMetricsDTO GenderMetrics { get; set; }
        public CompanyHealthMetricsDTO HealthMetrics { get; set; }
        public CompanyIndustryDTO[] Industries { get; set; }
        public CompanyJobMetricsDTO JobMetrics { get; set; }
        public CompanyKeyFinancialsMetricsDTO KeyFinancialsMetrics { get; set; }
        public CompanyNameDTO[] Names { get; set; }
        public CompanyRaceMetricsDTO RaceMetrics { get; set; }
        public RoleDTO[] Roles { get; set; }
    }
}
