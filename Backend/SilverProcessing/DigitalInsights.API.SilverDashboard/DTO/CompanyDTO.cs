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

        public int Id { get; private set; }
        public string LegalName { get; private set; }
        public string Lei { get; private set; }
        public AddressDTO[] Addresses { get; private set; }
        public CompanyBoardStatisticsDTO BoardStatistics { get; set; }
        public CompanyCountryDTO[] Countries { get; }
        public CompanyDIMetricsDTO DiMetrics { get; }
        public CompanyExecutiveStatisticsDTO ExecutiveStatistics { get; }
        public CompanyGenderMetricsDTO GenderMetrics { get; }
        public CompanyHealthMetricsDTO HealthMetrics { get; }
        public CompanyIndustryDTO[] Industries { get; }
        public CompanyJobMetricsDTO JobMetrics { get; }
        public CompanyKeyFinancialsMetricsDTO KeyFinancialsMetrics { get; set; }
        public CompanyNameDTO[] Names { get; set; }
        public CompanyRaceMetricsDTO RaceMetrics { get; set; }
        public RoleDTO[] Roles { get; set; }
    }
}
