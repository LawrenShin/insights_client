using DigitalInsights.DB.Silver.Entities;
using Newtonsoft.Json;
using System.Linq;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyDTO
    {
        public CompanyDTO()
        {
            CompanyCountries = new CompanyCountryDTO[0];
            CompanyNames = new CompanyNameDTO[0];
            CompanyIndustries = new CompanyIndustryDTO[0];
            Roles = new RoleDTO[0];
        }

        public CompanyDTO(Company source)
        {
            Id = source.Id;
            LegalName = source.LegalName;
            LEI = source.Lei;

            var publicData = source.CompanyPublicData.FirstOrDefault();
            if (publicData != null)
            {
                AllEmployeesGenderRatioFemale = publicData.AllEmployeesGenderRatioFemale;
                AllEmployeesVisibleRaceMinority = publicData.AllEmployeesVisibleRaceMinority;
                BoardVisibleRaceMinority = publicData.BoardVisibleRaceMinority;
                CompanyHasProgramForAdvancingMinorities = publicData.CompanyHasProgramForAdvancingMinorities;
                CompanyHasSocialImpactPrograms = publicData.CompanyHasSocialImpactPrograms;
                CompanyMeasuresEngagement  = publicData.CompanyMeasuresEngagement;
                CompanyOffersTraining = publicData.CompanyOffersTraining;
                CompanySupplierSpendingWithDi = publicData.CompanySupplierSpendingWithDi;
                DIOnWebsite = publicData.DIOnWebsite;
                EngagementSurvey = publicData.EngagementSurvey;
                EngagementSurveyResponseRate = publicData.EngagementSurveyResponseRate;
                ExecutivesVisibleRaceMinority = publicData.ExecutivesVisibleRaceMinority;
                Fatalities = publicData.Fatalities;
                GenderPayGapFemale = publicData.GenderPayGapFemale;
                HQAddress = new AddressDTO(publicData.HqAddress, publicData.HqAddressEditable);
                LegalAddress = new AddressDTO(publicData.LegalAddress, publicData.LegalAddressEditable);
                InvoluntaryTurnoverRate = publicData.InvoluntaryTurnoverRate;
                MiddleMgmtGenderRatioFemale = publicData.MiddleMgmtGenderRatioFemale;
                MiddleMgmtVisibleRaceMinority = publicData.MiddleMgmtVisibleRaceMinority;
                NumEmployees = publicData.NumEmployees;
                SeniorMgmtGenderRatioFemale = publicData.SeniorMgmtGenderRatioFemale;
                SicknessAbsense = publicData.SicknessAnsense;
                TotalHoursWorked = publicData.TotalHoursWorked;
                TotalRecordableInjuries = publicData.TotalRecordableInjuries;
                TotalTurnoverRate = publicData.TotalTurnoverRate;
                VoluntaryTurnoverRate = publicData.VoluntaryTurnoverRate;
            }

            CompanyCountries = source.CompanyCountries.Select(x => new CompanyCountryDTO(x)).ToArray();

            CompanyNames = source.CompanyNames.Select(x => new CompanyNameDTO(x)).ToArray();

            CompanyIndustries = source.CompanyIndustries.Select(x=>new CompanyIndustryDTO(x)).ToArray();

            Roles = source.Roles.Select(x => new RoleDTO(x)).ToArray();
        }

        [JsonProperty("otherNames")]
        public CompanyNameDTO[] CompanyNames { get; set; }
        [JsonProperty("industries")]
        public CompanyIndustryDTO[] CompanyIndustries { get; set; }
        [JsonProperty("roles")]
        public RoleDTO[] Roles { get; set; }
        [JsonProperty("countries")]
        public CompanyCountryDTO[] CompanyCountries { get; set; }

        [JsonProperty("id")]
        public int Id { get; private set; }
        [JsonProperty("legalName")]
        public string LegalName { get; private set; }
        [JsonProperty("lei")]
        public string LEI { get; private set; }
        [JsonProperty("numberOfEmployees")]
        public int? NumEmployees { get; private set; }
        [JsonProperty("seniorManagementGenderRatioFemale")]
        public double? SeniorMgmtGenderRatioFemale { get; private set; }
        [JsonProperty("totalHoursWorked")]
        public int? TotalHoursWorked { get; private set; }
        [JsonProperty("sicknessAbsense")]
        public double? SicknessAbsense { get; private set; }
        [JsonProperty("totalRecordableInjuries")]
        public int? TotalRecordableInjuries { get; private set; }
        [JsonProperty("totalTurnoverRate")]
        public double? TotalTurnoverRate { get; private set; }
        [JsonProperty("voluntaryTurnoverRate")]
        public double? VoluntaryTurnoverRate { get; private set; }
        [JsonProperty("executivesVisibleRaceMinority")]
        public double? ExecutivesVisibleRaceMinority { get; private set; }
        [JsonProperty("fatalities")]
        public int? Fatalities { get; private set; }
        [JsonProperty("genderPayGapFemale")]
        public double? GenderPayGapFemale { get; private set; }
        [JsonProperty("hqAddress")]
        public AddressDTO HQAddress { get; private set; }
        [JsonProperty("legalAddress")]
        public AddressDTO LegalAddress { get; private set; }
        [JsonProperty("allEmployeesGenderRatioFemale")]
        public double? AllEmployeesGenderRatioFemale { get; private set; }
        [JsonProperty("allEmployeesVisibleRaceMinority")]
        public double? AllEmployeesVisibleRaceMinority { get; private set; }
        [JsonProperty("boardVisibleRaceMinority")]
        public double? BoardVisibleRaceMinority { get; private set; }
        [JsonProperty("companyHasProgramForAdvancingMinorities")]
        public bool? CompanyHasProgramForAdvancingMinorities { get; private set; }
        [JsonProperty("involuntaryTurnoverRate")]
        public double? InvoluntaryTurnoverRate { get; private set; }
        [JsonProperty("middleManagementGenderRatioFemale")]
        public double? MiddleMgmtGenderRatioFemale { get; private set; }
        [JsonProperty("middleManagementVisibleRaceMinority")]
        public double? MiddleMgmtVisibleRaceMinority { get; private set; }
        [JsonProperty("companyHasSocialImpactPrograms")]
        public bool? CompanyHasSocialImpactPrograms { get; private set; }
        [JsonProperty("companyMeasuresEngagement")]
        public bool? CompanyMeasuresEngagement { get; private set; }
        [JsonProperty("companyOffersTraining")]
        public bool? CompanyOffersTraining { get; private set; }
        [JsonProperty("companySupplierSpendingWithDi")]
        public double? CompanySupplierSpendingWithDi { get; private set; }
        [JsonProperty("diOnWbsite")]
        public bool? DIOnWebsite { get; private set; }
        [JsonProperty("engagementSurvey")]
        public double? EngagementSurvey { get; private set; }
        [JsonProperty("engagementSurveyResponseRate")]
        public double? EngagementSurveyResponseRate { get; private set; }
    }
}
