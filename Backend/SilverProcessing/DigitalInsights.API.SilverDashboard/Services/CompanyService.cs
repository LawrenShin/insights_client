using DigitalInsights.API.SilverDashboard.DTO;
using DigitalInsights.DB.Common.Enums;
using DigitalInsights.DB.Silver;
using DigitalInsights.DB.Silver.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.Services
{
    public class CompanyService
    {
        SilverContext silverContext;

        HashSet<int> countries;

        public CompanyService(SilverContext context)
        {
            silverContext = context;
            countries = silverContext.Countries.Select(x => x.Id).ToHashSet();
        }

        public CompaniesDTO GetCompanies(
            int pageSize,
            int pageIndex,
            string searchPrefix)
        {
            var companiesQuery = silverContext.Companies.AsQueryable<Company>();

            if (!string.IsNullOrEmpty(searchPrefix))
            {
                companiesQuery = companiesQuery.Where(x => x.LegalName.StartsWith(searchPrefix));
            }

            return new CompaniesDTO(
                companiesQuery
                    .Include(x => x.Roles)
                    .Include(x => x.CompanyCountries).ThenInclude(x => x.Country)
                    .Include(x => x.CompanyIndustries)
                    .Include(x => x.CompanyNames)
                    .Include(x => x.CompanyBoardStatistics)
                    .Include(x => x.CompanyExecutiveStatistics)
                    .Include(x => x.CompanyKeyFinancialsMetrics)
                    .Include(x => x.CompanyHealthMetrics)
                    .Include(x => x.CompanyJobMetrics)
                    .Include(x => x.CompanyGenderMetrics)
                    .Include(x => x.CompanyRaceMetrics)
                    .Include(x => x.CompanyDIMetrics)
                    .Include(x => x.CompanyHealthMetrics)
                    .OrderBy(x => x.LegalName).Skip(pageIndex * pageSize).Take(pageSize).ToArray(),
                pageSize,
                pageIndex,
                (int)Math.Ceiling((double)silverContext.Companies.Count() / pageSize));
        }

        public void DeleteCompany(int id)
        {
            var company = silverContext.Companies.Where(x => x.Id == id).FirstOrDefault();

            if (company == null)
            {
                throw new ArgumentException("id");
            }

            silverContext.Companies.Remove(company);
            silverContext.SaveChanges();
        }

        public void UpdateOrInsertCompany(CompanyDTO source)
        {
            Company targetCompany;

            if (source.Id == 0)
            {
                targetCompany = new Company();
                silverContext.Companies.Add(targetCompany);
            }
            else
            {
                targetCompany = silverContext.Companies.Where(x => x.Id == source.Id)
                    .Include(x => x.CompanyCountries)
                    .Include(x => x.CompanyPublicData)
                    .Include(x => x.CompanyIndustries)
                    .Include(x => x.CompanyNames)
                    .Include(x => x.Roles)
                    .FirstOrDefault();

                if (targetCompany == null)
                {
                    throw new ArgumentOutOfRangeException("company");
                }
            }

            targetCompany.Lei = source.LEI;
            targetCompany.LegalName = source.LegalName;

            FillCompanyCountries(source, targetCompany);
            FillCompanyNames(source, targetCompany);
            FillCompanyIndustries(source, targetCompany);
            FillPublicData(source, targetCompany);
            FillRoles(source, targetCompany);

            silverContext.SaveChanges();
        }

        private void FillPublicData(CompanyDTO source, Company targetCompany)
        {
            CompanyPublicData publicData;

            if (source.AllEmployeesGenderRatioFemale.HasValue && (source.AllEmployeesGenderRatioFemale < 0 || source.AllEmployeesGenderRatioFemale > 100)
                || source.AllEmployeesVisibleRaceMinority.HasValue && (source.AllEmployeesVisibleRaceMinority < 0 || source.AllEmployeesVisibleRaceMinority > 100)
                || source.BoardVisibleRaceMinority.HasValue && (source.BoardVisibleRaceMinority < 0 || source.BoardVisibleRaceMinority > 100)
                || source.CompanySupplierSpendingWithDi.HasValue && (source.CompanySupplierSpendingWithDi < 0 || source.CompanySupplierSpendingWithDi > 100)
                || source.EngagementSurvey.HasValue && (source.EngagementSurvey < 0 || source.EngagementSurvey > 100)
                || source.EngagementSurveyResponseRate.HasValue && (source.EngagementSurveyResponseRate < 0 || source.EngagementSurveyResponseRate > 100)
                || source.ExecutivesVisibleRaceMinority.HasValue && (source.ExecutivesVisibleRaceMinority < 0 || source.ExecutivesVisibleRaceMinority > 100)
                || source.Fatalities.HasValue && source.Fatalities < 0
                || source.HQAddress == null
                    || source.HQAddress.CountryId == null || !countries.Contains(source.HQAddress.CountryId.Value)
                || source.LegalAddress == null
                    || source.LegalAddress.CountryId == null || !countries.Contains(source.HQAddress.CountryId.Value)
                || source.InvoluntaryTurnoverRate.HasValue && source.InvoluntaryTurnoverRate < 0
                || source.MiddleMgmtGenderRatioFemale.HasValue && (source.MiddleMgmtGenderRatioFemale < 0 || source.MiddleMgmtGenderRatioFemale > 100)
                || source.MiddleMgmtVisibleRaceMinority.HasValue && (source.MiddleMgmtVisibleRaceMinority < 0 || source.MiddleMgmtVisibleRaceMinority > 100)
                || source.NumEmployees.HasValue && source.NumEmployees < 0
                || source.SeniorMgmtGenderRatioFemale.HasValue && (source.SeniorMgmtGenderRatioFemale < 0 || source.SeniorMgmtGenderRatioFemale > 100)
                || source.SicknessAbsense.HasValue && source.SicknessAbsense < 0
                || source.TotalHoursWorked.HasValue && source.TotalHoursWorked < 0
                || source.TotalRecordableInjuries.HasValue && source.TotalRecordableInjuries < 0
                || source.TotalTurnoverRate.HasValue && (source.TotalTurnoverRate < 0 || source.TotalTurnoverRate > 100)
                || source.VoluntaryTurnoverRate.HasValue && (source.VoluntaryTurnoverRate < 0 || source.VoluntaryTurnoverRate > 100))
            {
                throw new ArgumentException("company country");
            }

            if (targetCompany.CompanyPublicData.Count == 1)
            {
                publicData = targetCompany.CompanyPublicData.First();
            }
            else
            {
                if (targetCompany.CompanyPublicData.Count > 1)
                {
                    silverContext.CompanyPublicData.RemoveRange(targetCompany.CompanyPublicData);
                    targetCompany.CompanyPublicData.Clear();
                }

                publicData = new CompanyPublicData()
                {
                    Company = targetCompany,
                    HqAddressEditable = true,
                    LegalAddressEditable = true,
                };
                silverContext.Add(publicData);
                targetCompany.CompanyPublicData.Add(publicData);
            }

            // simple fields
            publicData.AllEmployeesGenderRatioFemale = source.AllEmployeesGenderRatioFemale;
            publicData.AllEmployeesVisibleRaceMinority = source.AllEmployeesVisibleRaceMinority;
            publicData.BoardVisibleRaceMinority = source.BoardVisibleRaceMinority;
            publicData.CompanyHasProgramForAdvancingMinorities = source.CompanyHasProgramForAdvancingMinorities;
            publicData.CompanyHasSocialImpactPrograms = source.CompanyHasSocialImpactPrograms;
            publicData.CompanyMeasuresEngagement = source.CompanyMeasuresEngagement;
            publicData.CompanyOffersTraining = source.CompanyOffersTraining;
            publicData.CompanySupplierSpendingWithDi = source.CompanySupplierSpendingWithDi;
            publicData.DIOnWebsite = source.DIOnWebsite;
            publicData.EngagementSurvey = source.EngagementSurvey;
            publicData.EngagementSurveyResponseRate = source.EngagementSurveyResponseRate;
            publicData.ExecutivesVisibleRaceMinority = source.ExecutivesVisibleRaceMinority;
            publicData.Fatalities = source.Fatalities;
            publicData.GenderPayGapFemale = source.GenderPayGapFemale;
            publicData.InvoluntaryTurnoverRate = source.InvoluntaryTurnoverRate;
            publicData.MiddleMgmtGenderRatioFemale = source.MiddleMgmtGenderRatioFemale;
            publicData.MiddleMgmtVisibleRaceMinority = source.MiddleMgmtVisibleRaceMinority;
            publicData.NumEmployees = source.NumEmployees;
            publicData.SeniorMgmtGenderRatioFemale = source.SeniorMgmtGenderRatioFemale;
            publicData.SicknessAnsense = source.SicknessAbsense;
            publicData.TotalHoursWorked = source.TotalHoursWorked;
            publicData.TotalRecordableInjuries = source.TotalRecordableInjuries;
            publicData.TotalTurnoverRate = source.TotalTurnoverRate;
            publicData.VoluntaryTurnoverRate = source.VoluntaryTurnoverRate;

            // addresses
            if (publicData.HqAddressEditable)
            {
                if(publicData.HqAddress == null)
                {
                    publicData.HqAddress = new Address();
                    silverContext.Add(publicData.HqAddress);
                }
                Address targetAddress = publicData.HqAddress;
                AddressDTO sourceAddress = source.HQAddress;

                targetAddress.AddressLine = sourceAddress.AddressLine;
                targetAddress.AddressNumber = sourceAddress.AddressNumber;
                targetAddress.City = sourceAddress.City;
                targetAddress.CountryId = sourceAddress.CountryId.Value;
                targetAddress.PostalCode = sourceAddress.PostalCode;
                targetAddress.Region = sourceAddress.Region;
            }

            if (publicData.LegalAddressEditable)
            {
                if (publicData.LegalAddress == null)
                {
                    publicData.LegalAddress = new Address();
                    silverContext.Add(publicData.LegalAddress);
                }
                Address targetAddress = publicData.LegalAddress;
                AddressDTO sourceAddress = source.LegalAddress;

                targetAddress.AddressLine = sourceAddress.AddressLine;
                targetAddress.AddressNumber = sourceAddress.AddressNumber;
                targetAddress.City = sourceAddress.City;
                targetAddress.CountryId = sourceAddress.CountryId.Value;
                targetAddress.PostalCode = sourceAddress.PostalCode;
                targetAddress.Region = sourceAddress.Region;
            }
        }

        private void FillCompanyCountries(CompanyDTO source, Company targetCompany)
        {
            // countries
            if (source.CompanyCountries.Any(
                x => x == null
                || x.CountryId == null || !countries.Contains(x.CountryId.Value)
                || x.IsPrimary == null
                || x.LegalJurisdiction == null))
            {
                throw new ArgumentException("company country");
            }

            var srcIds = source.CompanyCountries
                .Select(x => x.CountryId.Value)
                .ToHashSet();

            var toRemove = targetCompany.CompanyCountries.Where(x => !srcIds.Contains(x.CountryId)).ToList();

            foreach (var item in toRemove)
            {
                targetCompany.CompanyCountries.Remove(item);
                silverContext.Remove(item);
            }

            var targetCompanyCountries = targetCompany.CompanyCountries.ToDictionary(x => x.CountryId, x => x);

            foreach (var companyCountry in source.CompanyCountries)
            {
                CompanyCountry targetEntity;

                if (!targetCompanyCountries.ContainsKey(companyCountry.CountryId.Value))
                {
                    targetEntity = new CompanyCountry()
                    {
                        Company = targetCompany
                    };

                    targetCompany.CompanyCountries.Add(targetEntity);
                    silverContext.CompanyCountries.Add(targetEntity);
                }
                else
                {
                    targetEntity = targetCompanyCountries[companyCountry.CountryId.Value];
                }
                targetEntity.IsPrimary = companyCountry.IsPrimary.Value;
                targetEntity.LegalJurisdiction = companyCountry.LegalJurisdiction.Value;
                targetEntity.StockIndex = companyCountry.StockIndex;
                targetEntity.Ticker = companyCountry.Ticker;
            }
        }

        private void FillCompanyIndustries(CompanyDTO source, Company targetCompany)
        {
            // industries
            var industries = silverContext.Industries.Select(x => x.Id).ToHashSet();
            var industryCodes = silverContext.Industries.Select(x => x.Id).ToHashSet();

            if (source.CompanyIndustries.Any(
                x => x == null
                || x.PrimarySecondary == null
                || x.Industry == null || !industries.Contains(x.Industry.Value)
                || x.IndustryCode != null && !industryCodes.Contains(x.IndustryCode.Value)))
            {
                throw new ArgumentException("company industries");
            }

            var srcIds = source.CompanyIndustries
                .Select(x => x.Industry.Value)
                .ToHashSet();

            var toRemove = targetCompany.CompanyIndustries.Where(x => !srcIds.Contains((int)x.Industry)).ToList();

            foreach (var item in toRemove)
            {
                targetCompany.CompanyIndustries.Remove(item);
                silverContext.Remove(item);
            }

            var targetCompanyIndustries = targetCompany.CompanyIndustries.ToDictionary(x => (int)x.Industry, x => x);

            foreach (var companyIndustry in source.CompanyIndustries)
            {
                CompanyIndustry targetEntity;

                if (!targetCompanyIndustries.ContainsKey(companyIndustry.Industry.Value))
                {
                    targetEntity = new CompanyIndustry()
                    {
                        Company = targetCompany,
                        Industry = (DB.Common.Enums.Industry)companyIndustry.Industry.Value,
                    };

                    targetCompany.CompanyIndustries.Add(targetEntity);
                    silverContext.CompanyIndustries.Add(targetEntity);
                }
                else
                {
                    targetEntity = targetCompanyIndustries[companyIndustry.Industry.Value];
                }

                targetEntity.IndustryCode = (IndustryCode)companyIndustry.IndustryCode.Value;
                targetEntity.PrimarySecondary = companyIndustry.PrimarySecondary.Value;
            }
        }

        private void FillCompanyNames(CompanyDTO source, Company targetCompany)
        {
            // names

            if (source.CompanyNames.Any(
                x => x == null
                || string.IsNullOrEmpty(x.NameType)
                || string.IsNullOrEmpty(x.Name)))
            {
                throw new ArgumentException("company names");
            }

            var toRemove = targetCompany.CompanyNames.Where(x => !source.CompanyNames.Any(y=>y.Name == x.Name && x.NameType == y.NameType)).ToList();

            foreach (var item in toRemove)
            {
                targetCompany.CompanyNames.Remove(item);
                silverContext.Remove(item);
            }

            var toAdd = source.CompanyNames.Where(x => !targetCompany.CompanyNames.Any(y => y.Name == x.Name && x.NameType == y.NameType)).ToList();

            foreach (var companyName in toAdd)
            {
                CompanyName targetEntity = new CompanyName()
                {
                    Company = targetCompany
                };
                
                targetCompany.CompanyNames.Add(targetEntity);
                silverContext.CompanyNames.Add(targetEntity);
                
                targetEntity.Name = companyName.Name;
                targetEntity.NameType = companyName.NameType;
            }
        }

        private void FillRoles(CompanyDTO source, Company targetCompany)
        {
            // roles
            var roleTypes = Enum.GetValues<RoleType>().Select(x => (int)x).ToArray();
            if (source.Roles.Any(
                x => x == null
                || x.PersonId == null
                || !silverContext.People.Any(y => y.Id == x.PersonId)
                || x.RoleType == null 
                || !roleTypes.Contains(x.RoleType.Value)
                || string.IsNullOrEmpty(x.Title)))
            {
                throw new ArgumentException("roles");
            }

            var toRemove = targetCompany.Roles.Where(x => !source.Roles.Any(y => y.RoleType.Value == (int)x.RoleType && x.Title == y.Title)).ToList();

            foreach (var item in toRemove)
            {
                targetCompany.Roles.Remove(item);
                silverContext.Remove(item);
            }

            var toUpsert = source.Roles
                .ToDictionary(x => x, y => targetCompany.Roles.FirstOrDefault(x => y.RoleType.Value == (int)x.RoleType && x.Title == y.Title));

            foreach (var role in toUpsert.Keys)
            {
                Role targetEntity;
                if (toUpsert[role] == null)
                {
                    targetEntity = new Role()
                    {
                        Company = targetCompany,
                        RoleType = (RoleType)role.RoleType.Value,
                        Title = role.Title
                    };

                    targetCompany.Roles.Add(targetEntity);
                    silverContext.Roles.Add(targetEntity);
                }
                else
                {
                    targetEntity = toUpsert[role];
                }
                
                targetEntity.BaseSalary = role.BaseSalary;
                targetEntity.IsEffective = role.IsEffective;
                targetEntity.OtherIncentives = role.OtherIncentives;
            }
        }
    }
}
