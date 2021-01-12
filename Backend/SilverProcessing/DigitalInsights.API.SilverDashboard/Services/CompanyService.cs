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
    internal class CompanyService
    {
        SilverContext silverContext;

        public CompanyService(SilverContext context)
        {
            silverContext = context;
        }

        public Company[] GetCompanies(
            int pageSize,
            int pageIndex,
            string searchPrefix)
        {
            var companiesQuery = silverContext.Companies.AsQueryable<Company>();

            string prefix = null;
            if (!string.IsNullOrEmpty(searchPrefix))
            {
                companiesQuery = companiesQuery.Where(x => x.LegalName.StartsWith(prefix));
            }

            return companiesQuery
                    .Include(x => x.Roles).ThenInclude(x => x.Person)
                    .Include(x => x.CompanyNames)
                    .Include(x => x.CompanyExtendedData)
                    .Include(x => x.CompanyQuestionnaires)
                    .OrderBy(x => x.LegalName).Skip(pageIndex * pageSize).Take(pageSize).ToArray();
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

        public void UpdateOrInsertCompany(Company source)
        {
            Company targetCompany;

            if (!source.Id.HasValue || source.Id == 0)
            {
                targetCompany = new Company();
                silverContext.Companies.Add(targetCompany);
            }
            else
            {
                targetCompany = silverContext.Companies.Where(x => x.Id == source.Id)
                    .Include(x => x.CompanyCountries)
                    .Include(x => x.CompanyExtendedData)
                    .Include(x => x.CompanyQuestionnaires)
                    .Include(x => x.CompanyIndustries)
                    .Include(x => x.CompanyNames)
                    .FirstOrDefault();

                if (targetCompany == null)
                {
                    throw new ArgumentOutOfRangeException("company");
                }
            }

            targetCompany.Lei = source.Lei;
            targetCompany.Status = source.Status;
            targetCompany.LegalName = source.LegalName;
            targetCompany.NumEmployees = source.NumEmployees;

            // countries

            var srcIds = source.CompanyCountries
                .Where(x => x.CompanyCountryId.HasValue)
                .Select(x => x.CompanyCountryId.Value)
                .ToHashSet();

            var toRemove = new List<CompanyCountry>();
            foreach (var companyCountry in targetCompany.CompanyCountries)
            {
                if (!srcIds.Contains(companyCountry.CompanyCountryId.Value))
                {
                    toRemove.Add(companyCountry);
                }
            }
            foreach (var item in toRemove)
            {
                targetCompany.CompanyCountries.Remove(item);
                silverContext.Remove(item);
            }

            foreach (var companyCountry in source.CompanyCountries)
            {
                CompanyCountry targetEntity;
                if (!companyCountry.CompanyCountryId.HasValue)
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
                    targetEntity = targetCompany.CompanyCountries.First(x => x.CompanyCountryId == companyCountry.CompanyCountryId);
                }
                targetEntity.CountryId = companyCountry.CountryId;
                targetEntity.IsPrimary = companyCountry.IsPrimary;
                targetEntity.LegalJurisdiction = companyCountry.LegalJurisdiction;
                targetEntity.Ticker = companyCountry.Ticker;
            }

            // extended data
            if (source.CompanyExtendedData.Count > 0)
            {
                var sourceExtendedData = source.CompanyExtendedData.First();
                CompanyExtendedData targetExtendedData;
                if (targetCompany.CompanyExtendedData.Any())
                {
                    targetExtendedData = targetCompany.CompanyExtendedData.First();
                }
                else
                {
                    targetExtendedData = new CompanyExtendedData();
                    targetCompany.CompanyExtendedData.Add(targetExtendedData);
                    silverContext.Add(targetExtendedData);
                }
                targetExtendedData.BelowNationalAvgIncome = sourceExtendedData.BelowNationalAvgIncome;
                targetExtendedData.Company = targetCompany;
                targetExtendedData.DisabledEmployees = sourceExtendedData.DisabledEmployees;
                targetExtendedData.HierarchyLevel = sourceExtendedData.HierarchyLevel;
                targetExtendedData.RetentionRate = sourceExtendedData.RetentionRate;
                targetExtendedData.MedianSalary = sourceExtendedData.MedianSalary;
            }
            else
            {
                targetCompany.CompanyExtendedData.Clear();
            }

            // questions
            if (source.CompanyQuestionnaires.Count > 0)
            {
                srcIds = source.CompanyQuestionnaires
                .Where(x => x.Id.HasValue)
                .Select(x => x.Id.Value)
                .ToHashSet();

                var questionsToRemove = new List<CompanyQuestion>();
                foreach (var companyQuestion in targetCompany.CompanyQuestionnaires)
                {
                    if (!srcIds.Contains(companyQuestion.Id.Value))
                    {
                        questionsToRemove.Add(companyQuestion);
                    }
                }
                foreach (var item in questionsToRemove)
                {
                    targetCompany.CompanyQuestionnaires.Remove(item);
                    silverContext.Remove(item);
                }

                foreach (var companyQuestion in source.CompanyQuestionnaires)
                {
                    CompanyQuestion targetEntity;
                    if (!companyQuestion.Id.HasValue)
                    {
                        targetEntity = new CompanyQuestion()
                        {
                            Company = targetCompany
                        };

                        targetCompany.CompanyQuestionnaires.Add(targetEntity);
                        silverContext.CompanyQuestionnaires.Add(targetEntity);
                    }
                    else
                    {
                        targetEntity = targetCompany.CompanyQuestionnaires.First(x => x.Id == companyQuestion.Id);
                    }
                    targetEntity.Question = companyQuestion.Question;
                    targetEntity.Answer = companyQuestion.Answer;
                }
            }
            else
            {
                targetCompany.CompanyQuestionnaires.Clear();
            }

            // names
            if (source.CompanyNames.Count > 0)
            {
                srcIds = source.CompanyNames
                .Where(x => x.Id.HasValue)
                .Select(x => x.Id.Value)
                .ToHashSet();

                var namesToRemove = new List<CompanyName>();
                foreach (var companyName in targetCompany.CompanyNames)
                {
                    if (!srcIds.Contains(companyName.Id.Value))
                    {
                        namesToRemove.Add(companyName);
                    }
                }
                foreach (var item in namesToRemove)
                {
                    targetCompany.CompanyNames.Remove(item);
                    silverContext.Remove(item);
                }

                foreach (var companyName in source.CompanyNames)
                {
                    CompanyName targetEntity;
                    if (!companyName.Id.HasValue)
                    {
                        targetEntity = new CompanyName()
                        {
                            Company = targetCompany
                        };

                        targetCompany.CompanyNames.Add(targetEntity);
                        silverContext.CompanyNames.Add(targetEntity);
                    }
                    else
                    {
                        targetEntity = targetCompany.CompanyNames.First(x => x.Id == companyName.Id);
                    }
                    targetEntity.Name = companyName.Name;
                    targetEntity.Type = companyName.Type;
                }
            }
            else
            {
                targetCompany.CompanyNames.Clear();
            }

            // industries
            if (source.CompanyIndustries.Count > 0)
            {
                srcIds = source.CompanyIndustries
                .Where(x => x.Id.HasValue)
                .Select(x => x.Id.Value)
                .ToHashSet();

                var industriesToRemove = new List<CompanyIndustry>();
                foreach (var companyName in targetCompany.CompanyIndustries)
                {
                    if (!srcIds.Contains(companyName.Id.Value))
                    {
                        industriesToRemove.Add(companyName);
                    }
                }
                foreach (var item in industriesToRemove)
                {
                    targetCompany.CompanyIndustries.Remove(item);
                    silverContext.Remove(item);
                }

                foreach (var companyIndustry in source.CompanyIndustries)
                {
                    CompanyIndustry targetEntity;
                    if (!companyIndustry.Id.HasValue)
                    {
                        targetEntity = new CompanyIndustry()
                        {
                            Company = targetCompany
                        };

                        targetCompany.CompanyIndustries.Add(targetEntity);
                        silverContext.CompanyIndustries.Add(targetEntity);
                    }
                    else
                    {
                        targetEntity = targetCompany.CompanyIndustries.First(x => x.Id == companyIndustry.Id);
                    }
                    targetEntity.IndustryId = companyIndustry.IndustryId;
                    targetEntity.PrimarySecondary = companyIndustry.PrimarySecondary;
                }
            }
            else
            {
                targetCompany.CompanyIndustries.Clear();
            }

            // TODO: roles

            silverContext.SaveChanges();
        }
    }
}
