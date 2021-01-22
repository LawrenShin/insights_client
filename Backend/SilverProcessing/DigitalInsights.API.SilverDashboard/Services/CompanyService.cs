using DigitalInsights.API.SilverDashboard.DTO;
using DigitalInsights.API.SilverDashboard.Helpers;
using DigitalInsights.DB.Common.Enums;
using DigitalInsights.DB.Silver;
using DigitalInsights.DB.Silver.Entities;
using DigitalInsights.DB.Silver.Entities.CompanyData;
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

            var result = companiesQuery
                    .Include(x => x.Roles)
                    .Include(x => x.CompanyCountries).ThenInclude(x => x.Country)
                    .Include(x => x.CompanyAddresses).ThenInclude(x=>x.Country)
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
                    .OrderBy(x => x.LegalName).Skip(pageIndex * pageSize).Take(pageSize).ToArray();

            foreach(var company in result)
            {
                if(company.CompanyBoardStatistics.Count ==0)
                {
                    company.CompanyBoardStatistics.Add(new CompanyBoardStatistics());
                }
                if (company.CompanyExecutiveStatistics.Count == 0)
                {
                    company.CompanyExecutiveStatistics.Add(new CompanyExecutiveStatistics());
                }
                if (company.CompanyKeyFinancialsMetrics.Count == 0)
                {
                    company.CompanyKeyFinancialsMetrics.Add(new CompanyKeyFinancialsMetrics());
                }
                if (company.CompanyHealthMetrics.Count == 0)
                {
                    company.CompanyHealthMetrics.Add(new CompanyHealthMetrics());
                }
                if (company.CompanyJobMetrics.Count == 0)
                {
                    company.CompanyJobMetrics.Add(new CompanyJobMetrics());
                }
                if (company.CompanyGenderMetrics.Count == 0)
                {
                    company.CompanyGenderMetrics.Add(new CompanyGenderMetrics());
                }
                if (company.CompanyRaceMetrics.Count == 0)
                {
                    company.CompanyRaceMetrics.Add(new CompanyRaceMetrics());
                }
                if (company.CompanyDIMetrics.Count == 0)
                {
                    company.CompanyDIMetrics.Add(new CompanyDIMetrics());
                }
            }

            return new CompaniesDTO(
                result,
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
                    .Include(x => x.Roles)
                    .Include(x => x.CompanyCountries)
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
                    .FirstOrDefault();

                if (targetCompany == null)
                {
                    throw new ArgumentOutOfRangeException("company");
                }
            }

            if(PropertyMetadataStorage.CurrentPropertyMetadata["company"].ContainsKey("lei")
                && PropertyMetadataStorage.CurrentPropertyMetadata["company"]["lei"].IsEditable)
                targetCompany.LEI = source.Lei;
            if (PropertyMetadataStorage.CurrentPropertyMetadata["company"].ContainsKey("legalname")
                && PropertyMetadataStorage.CurrentPropertyMetadata["company"]["legalname"].IsEditable)
                targetCompany.LegalName = source.LegalName;

            FillRoles(source, targetCompany);
            FillCompanyCountries(source, targetCompany);
            FillCompanyIndustries(source, targetCompany);
            FillCompanyNames(source, targetCompany);
            FillCompanyBoardStatistics(source, targetCompany);
            FillCompanyExecutiveStatistics(source, targetCompany);
            FillCompanyKeyFinancialsMetrics(source, targetCompany);
            FillCompanyHealthMetrics(source, targetCompany);
            FillCompanyJobMetrics(source, targetCompany);
            FillCompanyGenderMetrics(source, targetCompany);
            FillCompanyRaceMetrics(source, targetCompany);
            FillCompanyDIMetrics(source, targetCompany);
            FillCompanyHealthMetrics(source, targetCompany);

            silverContext.SaveChanges();
        }

        private void FillCompanyDIMetrics(CompanyDTO source, Company targetCompany)
        {
            if (source.CompanyDIMetrics == null || source.CompanyDIMetrics.Length == 0) return;

            var metricSource = source.CompanyDIMetrics[0];

            CompanyDIMetrics target;
            if (targetCompany.CompanyDIMetrics.Count == 1)
            {
                target = targetCompany.CompanyDIMetrics.First();
            }
            else
            {
                if (targetCompany.CompanyDIMetrics.Count > 1)
                {
                    silverContext.CompanyDIMetrics.RemoveRange(targetCompany.CompanyDIMetrics);
                    targetCompany.CompanyDIMetrics.Clear();
                }

                target = new CompanyDIMetrics()
                {
                    CompanyId = targetCompany.Id,
                };
                silverContext.Add(target);
                targetCompany.CompanyDIMetrics.Add(target);
            }

            target.DICodeConduct = metricSource.DICodeConduct;
            target.DIComplaint = metricSource.DIComplaint;
            target.DIDivision = metricSource.DIDivision;
            target.DIEarningCall = metricSource.DIEarningCall;
            target.DIFTEPosition = metricSource.DIFTEPosition;
            target.DIPolicyEstablished = metricSource.DIPolicyEstablished;
            target.DIPosition = metricSource.DIPosition;
            target.DIPositionExecutive = metricSource.DIPositionExecutive;
            target.DIPublicAvailable = metricSource.DIPublicAvailable;
            target.DISupplyChain = metricSource.DISupplyChain;
            target.DISupplySpendRevenueRatio = metricSource.DISupplySpendRevenueRatio;
            target.DITalentGoals = metricSource.DITalentGoals;
            target.DIWebsite = metricSource.DIWebsite;
            target.EmployEngagement = metricSource.EmployEngagement;
            target.EmploySatisfactionSurvey = metricSource.EmploySatisfactionSurvey;
            target.EmploySurveyResponseRate = metricSource.EmploySurveyResponseRate;
            target.HarassmentPolicy = metricSource.HarassmentPolicy;
            target.HolidaySupport = metricSource.HolidaySupport;
            target.ManagingDiverse = metricSource.ManagingDiverse;
            target.MentorProgram = metricSource.MentorProgram;
            target.Retaliation = metricSource.Retaliation;
            target.SocialEvents = metricSource.SocialEvents;
            target.SocialProgram = metricSource.SocialProgram;
            target.SupplySpend = metricSource.SupplySpend;
            target.ValueDISupplySpend = metricSource.ValueDISupplySpend;
        }

        private void FillCompanyRaceMetrics(CompanyDTO source, Company targetCompany)
        {
            if (source.CompanyRaceMetrics == null || source.CompanyRaceMetrics.Length == 0) return;

            var metricSource = source.CompanyRaceMetrics[0];

            CompanyRaceMetrics target;
            if (targetCompany.CompanyRaceMetrics.Count == 1)
            {
                target = targetCompany.CompanyRaceMetrics.First();
            }
            else
            {
                if (targetCompany.CompanyRaceMetrics.Count > 1)
                {
                    silverContext.CompanyRaceMetrics.RemoveRange(targetCompany.CompanyRaceMetrics);
                    targetCompany.CompanyRaceMetrics.Clear();
                }

                target = new CompanyRaceMetrics()
                {
                    CompanyId = targetCompany.Id,
                };
                silverContext.Add(target);
                targetCompany.CompanyRaceMetrics.Add(target);
            }

            target.RaceArab = metricSource.RaceArab;
            target.RaceAsian = metricSource.RaceAsian;
            target.RaceBlack = metricSource.RaceBlack;
            target.RaceCaucasian = metricSource.RaceCaucasian;
            target.RaceHispanic = metricSource.RaceHispanic;
            target.RaceIndigenous = metricSource.RaceIndigenous;
            target.RaceRatioAll = metricSource.RaceRatioAll;
            target.RaceRatioBoard = metricSource.RaceRatioBoard;
            target.RaceRatioExececutive = metricSource.RaceRatioExececutive;
            target.RaceRatioMiddle = metricSource.RaceRatioMiddle;
            target.RaceRatioSenior = metricSource.RaceRatioSenior;
        }

        private void FillCompanyGenderMetrics(CompanyDTO source, Company targetCompany)
        {
            if (source.CompanyGenderMetrics == null || source.CompanyGenderMetrics.Length == 0) return;

            var metricSource = source.CompanyGenderMetrics[0];

            CompanyGenderMetrics target;
            if (targetCompany.CompanyGenderMetrics.Count == 1)
            {
                target = targetCompany.CompanyGenderMetrics.First();
            }
            else
            {
                if (targetCompany.CompanyGenderMetrics.Count > 1)
                {
                    silverContext.CompanyGenderMetrics.RemoveRange(targetCompany.CompanyGenderMetrics);
                    targetCompany.CompanyGenderMetrics.Clear();
                }

                target = new CompanyGenderMetrics()
                {
                    CompanyId = targetCompany.Id,
                };
                silverContext.Add(target);
                targetCompany.CompanyGenderMetrics.Add(target);
            }

            target.GenderMale = metricSource.GenderMale;
            target.GenderOther = metricSource.GenderOther;
            target.GenderPayGap = metricSource.GenderPayGap;
            target.GenderRatioAll = metricSource.GenderRatioAll;
            target.GenderRatioBoard = metricSource.GenderRatioBoard;
            target.GenderRatioMiddle = metricSource.GenderRatioMiddle;
            target.GenderRatioSenior = metricSource.GenderRatioSenior;
        }

        private void FillCompanyJobMetrics(CompanyDTO source, Company targetCompany)
        {
            if (source.CompanyJobMetrics == null || source.CompanyJobMetrics.Length == 0) return;

            var metricSource = source.CompanyJobMetrics[0];

            CompanyJobMetrics target;
            if (targetCompany.CompanyJobMetrics.Count == 1)
            {
                target = targetCompany.CompanyJobMetrics.First();
            }
            else
            {
                if (targetCompany.CompanyJobMetrics.Count > 1)
                {
                    silverContext.CompanyJobMetrics.RemoveRange(targetCompany.CompanyJobMetrics);
                    targetCompany.CompanyJobMetrics.Clear();
                }

                target = new CompanyJobMetrics()
                {
                    CompanyId = targetCompany.Id,
                };
                silverContext.Add(target);
                targetCompany.CompanyJobMetrics.Add(target);
            }

            target.AverageSalary = metricSource.AverageSalary;
            target.EmployTraining = metricSource.EmployTraining;
            target.EmployTurnoverFired = metricSource.EmployTurnoverFired;
            target.EmployTurnoverTotal = metricSource.EmployTurnoverTotal;
            target.EmployTurnoverVoluntary = metricSource.EmployTurnoverVoluntary;
            target.JobTenureAverage = metricSource.JobTenureAverage;
            target.MedianSalary = metricSource.MedianSalary;
            target.TotalHours = metricSource.TotalHours;
        }

        private void FillCompanyHealthMetrics(CompanyDTO source, Company targetCompany)
        {
            if (source.CompanyHealthMetrics == null || source.CompanyHealthMetrics.Length == 0) return;

            var metricSource = source.CompanyHealthMetrics[0];

            CompanyHealthMetrics target;
            if (targetCompany.CompanyHealthMetrics.Count == 1)
            {
                target = targetCompany.CompanyHealthMetrics.First();
            }
            else
            {
                if (targetCompany.CompanyHealthMetrics.Count > 1)
                {
                    silverContext.CompanyHealthMetrics.RemoveRange(targetCompany.CompanyHealthMetrics);
                    targetCompany.CompanyHealthMetrics.Clear();
                }

                target = new CompanyHealthMetrics()
                {
                    CompanyId = targetCompany.Id,
                };
                silverContext.Add(target);
                targetCompany.CompanyHealthMetrics.Add(target);
            }

            target.AgeAverage = metricSource.AgeAverage;
            target.Fatalities = metricSource.Fatalities;
            target.HealthTRI = metricSource.HealthTRI;
            target.HealthTRIR = metricSource.HealthTRIR;
            target.SickAbsence = metricSource.SickAbsence;
        }

        private void FillCompanyBoardStatistics(CompanyDTO source, Company targetCompany)
        {
            if (source.CompanyBoardStatistics == null || source.CompanyBoardStatistics.Length == 0) return;

            var metricSource = source.CompanyBoardStatistics[0];

            CompanyBoardStatistics target;
            if (targetCompany.CompanyBoardStatistics.Count == 1)
            {
                target = targetCompany.CompanyBoardStatistics.First();
            }
            else
            {
                if (targetCompany.CompanyBoardStatistics.Count > 1)
                {
                    silverContext.CompanyBoardStatistics.RemoveRange(targetCompany.CompanyBoardStatistics);
                    targetCompany.CompanyBoardStatistics.Clear();
                }

                target = new CompanyBoardStatistics()
                {
                    CompanyId = targetCompany.Id,
                };
                silverContext.Add(target);
                targetCompany.CompanyBoardStatistics.Add(target);
            }

            target.ArabPercentage = metricSource.ArabPercentage;
            target.AsianPercentage = metricSource.AsianPercentage;
            target.AverageAge = metricSource.AverageAge;
            target.AverageEducationLength = metricSource.AverageEducationLength;
            target.BlackPercentage = metricSource.BlackPercentage;
            target.CaucasianPercentage = metricSource.CaucasianPercentage;
            target.FemaleRatio = metricSource.FemaleRatio;
            target.Height = metricSource.Height;
            target.HispanicPercentage = metricSource.HispanicPercentage;
            target.IndigenousPercentage = metricSource.IndigenousPercentage;
            target.MembersNumber = metricSource.MembersNumber;
            target.SalaryAverage = metricSource.SalaryAverage;
            target.SalaryMean = metricSource.SalaryMean;
            target.Weight = metricSource.Weight;
        }

        private void FillCompanyExecutiveStatistics(CompanyDTO source, Company targetCompany)
        {
            if (source.CompanyExecutiveStatistics == null || source.CompanyExecutiveStatistics.Length == 0) return;

            var metricSource = source.CompanyExecutiveStatistics[0];

            CompanyExecutiveStatistics target;
            if (targetCompany.CompanyExecutiveStatistics.Count == 1)
            {
                target = targetCompany.CompanyExecutiveStatistics.First();
            }
            else
            {
                if (targetCompany.CompanyExecutiveStatistics.Count > 1)
                {
                    silverContext.CompanyExecutiveStatistics.RemoveRange(targetCompany.CompanyExecutiveStatistics);
                    targetCompany.CompanyExecutiveStatistics.Clear();
                }

                target = new CompanyExecutiveStatistics()
                {
                    CompanyId = targetCompany.Id,
                };
                silverContext.Add(target);
                targetCompany.CompanyExecutiveStatistics.Add(target);
            }

            target.ArabPercentage = metricSource.ArabPercentage;
            target.AsianPercentage = metricSource.AsianPercentage;
            target.AverageAge = metricSource.AverageAge;
            target.AverageEducationLength = metricSource.AverageEducationLength;
            target.BlackPercentage = metricSource.BlackPercentage;
            target.CaucasianPercentage = metricSource.CaucasianPercentage;
            target.FemaleRatio = metricSource.FemaleRatio;
            target.Height = metricSource.Height;
            target.HispanicPercentage = metricSource.HispanicPercentage;
            target.IndigenousPercentage = metricSource.IndigenousPercentage;
            target.MembersNumber = metricSource.MembersNumber;
            target.SalaryAverage = metricSource.SalaryAverage;
            target.SalaryMean = metricSource.SalaryMean;
            target.Weight = metricSource.Weight;
        }

        private void FillCompanyKeyFinancialsMetrics(CompanyDTO source, Company targetCompany)
        {
            if (source.CompanyKeyFinancialsMetrics == null || source.CompanyKeyFinancialsMetrics.Length == 0) return;

            var metricSource = source.CompanyKeyFinancialsMetrics[0];

            CompanyKeyFinancialsMetrics target;
            if (targetCompany.CompanyKeyFinancialsMetrics.Count == 1)
            {
                target = targetCompany.CompanyKeyFinancialsMetrics.First();
            }
            else
            {
                if (targetCompany.CompanyKeyFinancialsMetrics.Count > 1)
                {
                    silverContext.CompanyKeyFinancialsMetrics.RemoveRange(targetCompany.CompanyKeyFinancialsMetrics);
                    targetCompany.CompanyKeyFinancialsMetrics.Clear();
                }

                target = new CompanyKeyFinancialsMetrics()
                {
                    CompanyId = targetCompany.Id,
                };
                silverContext.Add(target);
                targetCompany.CompanyKeyFinancialsMetrics.Add(target);
            }

            target.Employees = metricSource.Employees;
            target.OperatingRevenue = metricSource.OperatingRevenue;
            target.TotalAssets = metricSource.TotalAssets;
        }

        private void FillCompanyCountries(CompanyDTO source, Company targetCompany)
        {
            // countries
            var srcIds = source.CompanyCountries
                .Select(x => x.Country)
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

                if (!targetCompanyCountries.ContainsKey(companyCountry.Country))
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
                    targetEntity = targetCompanyCountries[companyCountry.Country];
                }
                targetEntity.Ticker = companyCountry.Ticker;
            }
        }

        private void FillCompanyIndustries(CompanyDTO source, Company targetCompany)
        {
            // industries
            var industries = silverContext.Industries.Select(x => x.Id).ToHashSet();
            var industryCodes = silverContext.Industries.Select(x => x.Id).ToHashSet();

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
                targetEntity.IsPrimary = companyIndustry.IsPrimary.Value;
                targetEntity.TradeDescription = companyIndustry.TradeDescription;
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
                targetEntity.JobTenure = role.JobTenure;
                targetEntity.OtherIncentives = role.OtherIncentives;
            }
        }
    }
}
