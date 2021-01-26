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

            var properties = PropertyMetadataStorage.CurrentPropertyMetadata["person"];
            foreach (var property in properties.Values)
            {
                switch(property.PropertyName)
                {
                    case "lei":
                        {
                            ValidationHelper.ValidateAndSetProperty(property, () => source.Lei, x => targetCompany.LEI = x);
                            break;
                        }
                    case "legalname":
                        {
                            ValidationHelper.ValidateAndSetProperty(property, () => source.LegalName, x => targetCompany.LegalName = x);
                            break;
                        }
                    case "roles":
                        {
                            if(property.IsEditable)
                            {
                                if (!property.AllowsNull && source.Roles == null)
                                    throw new ArgumentException($"{property.EntityName} {property.PropertyName}");
                                FillRoles(source, targetCompany);
                            }
                            break;
                        }
                    case "countries":
                        {
                            if (property.IsEditable)
                            {
                                if (!property.AllowsNull && source.Countries == null)
                                    throw new ArgumentException($"{property.EntityName} {property.PropertyName}");
                                FillCompanyCountries(source, targetCompany);
                            }
                            break;
                        }
                    case "industries":
                        {
                            if (property.IsEditable)
                            {
                                if (!property.AllowsNull && source.Industries == null)
                                    throw new ArgumentException($"{property.EntityName} {property.PropertyName}");
                                FillCompanyIndustries(source, targetCompany);
                            }
                            break;
                        }
                    case "names":
                        {
                            if (property.IsEditable)
                            {
                                if (!property.AllowsNull && source.Names == null)
                                    throw new ArgumentException($"{property.EntityName} {property.PropertyName}");
                                FillCompanyNames(source, targetCompany);
                            }
                            break;
                        }
                    case "boardstatistics":
                        {
                            if (property.IsEditable)
                            {
                                if (!property.AllowsNull && source.BoardStatistics == null)
                                    throw new ArgumentException($"{property.EntityName} {property.PropertyName}");
                                FillCompanyBoardStatistics(source, targetCompany);
                            }
                            break;
                        }
                    case "executivestatistics":
                        {
                            if (property.IsEditable)
                            {
                                if (!property.AllowsNull && source.ExecutiveStatistics == null)
                                    throw new ArgumentException($"{property.EntityName} {property.PropertyName}");
                                FillCompanyExecutiveStatistics(source, targetCompany);
                            }
                            break;
                        }
                    case "keyfinancialsmetrics":
                        {
                            if (property.IsEditable)
                            {
                                if (!property.AllowsNull && source.KeyFinancialsMetrics == null)
                                    throw new ArgumentException($"{property.EntityName} {property.PropertyName}");
                                FillCompanyKeyFinancialsMetrics(source, targetCompany);
                            }
                            break;
                        }
                    case "jobmetrics":
                        {
                            if (property.IsEditable)
                            {
                                if (!property.AllowsNull && source.JobMetrics == null)
                                    throw new ArgumentException($"{property.EntityName} {property.PropertyName}");
                                FillCompanyJobMetrics(source, targetCompany);
                            }
                            break;
                        }
                    case "gendermetrics":
                        {
                            if (property.IsEditable)
                            {
                                if (!property.AllowsNull && source.GenderMetrics == null)
                                    throw new ArgumentException($"{property.EntityName} {property.PropertyName}");
                                FillCompanyGenderMetrics(source, targetCompany);
                            }
                            break;
                        }
                    case "racemetrics":
                        {
                            if (property.IsEditable)
                            {
                                if (!property.AllowsNull && source.RaceMetrics == null)
                                    throw new ArgumentException($"{property.EntityName} {property.PropertyName}");
                                FillCompanyRaceMetrics(source, targetCompany);
                            }
                            break;
                        }
                    case "dimetrics":
                        {
                            if (property.IsEditable)
                            {
                                if (!property.AllowsNull && source.DiMetrics == null)
                                    throw new ArgumentException($"{property.EntityName} {property.PropertyName}");
                                FillCompanyDIMetrics(source, targetCompany);
                            }
                            break;
                        }
                    case "healthmetrics":
                        {
                            if (property.IsEditable)
                            {
                                if (!property.AllowsNull && source.HealthMetrics == null)
                                    throw new ArgumentException($"{property.EntityName} {property.PropertyName}");
                                FillCompanyHealthMetrics(source, targetCompany);
                            }
                            break;
                        }
                    default:
                        throw new NotSupportedException($"{property.EntityName} {property.PropertyName}");
                }
            }

            silverContext.SaveChanges();
        }

        private void FillCompanyDIMetrics(CompanyDTO source, Company targetCompany)
        {
            if (source.DiMetrics == null)
            {
                silverContext.CompanyDIMetrics.RemoveRange(targetCompany.CompanyDIMetrics);
            }
            else
            {
                var metricSource = source.DiMetrics;

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

                var properties = PropertyMetadataStorage.CurrentPropertyMetadata[typeof(CompanyDIMetrics).Name];
                foreach(var property in properties.Values)
                {
                    var result = (property.PropertyName.ToLower() switch
                    {
                        "dicodeconduct" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.DICodeConduct, x => target.DICodeConduct = x),
                        "dicomplaint" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.DIComplaint, x => target.DIComplaint = x),
                        "didivision" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.DIDivision, x => target.DIDivision = x),
                        "diearningcall" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.DIEarningCall, x => target.DIEarningCall = x),
                        "difteposition" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.DIFTEPosition, x => target.DIFTEPosition = x),
                        "dipolicyestablished" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.DIPolicyEstablished, x => target.DIPolicyEstablished = x),
                        "diposition" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.DIPosition, x => target.DIPosition = x),
                        "dipositionexecutive" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.DIPositionExecutive, x => target.DIPositionExecutive = x),
                        "dipublicavailable" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.DIPublicAvailable, x => target.DIPublicAvailable = x),
                        "disupplychain" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.DISupplyChain, x => target.DISupplyChain = x),
                        "disupplyspendrevenueratio" 
                            => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.DISupplySpendRevenueRatio, x => target.DISupplySpendRevenueRatio = x),
                        "ditalentgoals" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.DITalentGoals, x => target.DITalentGoals = x),
                        "diwebsite" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.DIWebsite, x => target.DIWebsite = x),
                        "employengagement" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.EmployEngagement, x => target.EmployEngagement = x),
                        "employsatisfactionsurvey" 
                            => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.EmploySatisfactionSurvey, x => target.EmploySatisfactionSurvey = x),
                        "employsurveyresponserate" 
                            => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.EmploySurveyResponseRate, x => target.EmploySurveyResponseRate = x),
                        "harassmentpolicy" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.HarassmentPolicy, x => target.HarassmentPolicy = x),
                        "holidaysupport" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.HolidaySupport, x => target.HolidaySupport = x),
                        "managingdiverse" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.ManagingDiverse, x => target.ManagingDiverse = x),
                        "mentorprogram" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.MentorProgram, x => target.MentorProgram = x),
                        "retaliation" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.Retaliation, x => target.Retaliation = x),
                        "socialevents" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.SocialEvents, x => target.SocialEvents = x),
                        "socialprogram" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.SocialProgram, x => target.SocialProgram = x),
                        "supplyspend" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.SupplySpend, x => target.SupplySpend = x),
                        "valuedisupplyspend" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.ValueDISupplySpend, x => target.ValueDISupplySpend = x),
                        _ => throw new NotSupportedException($"{property.EntityName} {property.PropertyName}"),
                    });
                }
            }
            
        }

        private void FillCompanyRaceMetrics(CompanyDTO source, Company targetCompany)
        {
            if (source.RaceMetrics == null)
            {
                silverContext.CompanyRaceMetrics.RemoveRange(targetCompany.CompanyRaceMetrics);
            }
            else
            {

                var metricSource = source.RaceMetrics;

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

                var properties = PropertyMetadataStorage.CurrentPropertyMetadata[typeof(CompanyRaceMetrics).Name];
                foreach (var property in properties.Values)
                {
                    var result = (property.PropertyName.ToLower() switch
                    {
                        "racearab" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.RaceArab, x => target.RaceArab = x),
                        "raceasian" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.RaceAsian, x => target.RaceAsian = x),
                        "raceblack" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.RaceBlack, x => target.RaceBlack = x),
                        "racecaucasian" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.RaceCaucasian, x => target.RaceCaucasian = x),
                        "racehispanic" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.RaceHispanic, x => target.RaceHispanic = x),
                        "raceindigenous" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.RaceIndigenous, x => target.RaceIndigenous = x),
                        "raceratioall" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.RaceRatioAll, x => target.RaceRatioAll = x),
                        "raceratioboard" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.RaceRatioBoard, x => target.RaceRatioBoard = x),
                        "raceratioexececutive" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.RaceRatioExececutive, x => target.RaceRatioExececutive = x),
                        "raceratiomiddle" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.RaceRatioMiddle, x => target.RaceRatioMiddle = x),
                        "raceratiosenior"
                            => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.RaceRatioSenior, x => target.RaceRatioSenior = x),
                        _ => throw new NotSupportedException($"{property.EntityName} {property.PropertyName}"),
                    });
                }
            }
        }

        private void FillCompanyGenderMetrics(CompanyDTO source, Company targetCompany)
        {
            if (source.GenderMetrics == null)
            {
                silverContext.CompanyGenderMetrics.RemoveRange(targetCompany.CompanyGenderMetrics);
            }
            else
            {
                var metricSource = source.GenderMetrics;

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

                var properties = PropertyMetadataStorage.CurrentPropertyMetadata[typeof(CompanyGenderMetrics).Name];
                foreach (var property in properties.Values)
                {
                    var result = (property.PropertyName.ToLower() switch
                    {
                        "gendermale" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.GenderMale, x => target.GenderMale = x),
                        "genderother" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.GenderOther, x => target.GenderOther = x),
                        "genderpaygap" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.GenderPayGap, x => target.GenderPayGap = x),
                        "genderratioall" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.GenderRatioAll, x => target.GenderRatioAll = x),
                        "genderratioboard" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.GenderRatioBoard, x => target.GenderRatioBoard = x),
                        "genderratiomiddle" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.GenderRatioMiddle, x => target.GenderRatioMiddle = x),
                        "genderratiosenior" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.GenderRatioSenior, x => target.GenderRatioSenior = x),
                        _ => throw new NotSupportedException($"{property.EntityName} {property.PropertyName}"),
                    });
                }
            }
        }

        private void FillCompanyJobMetrics(CompanyDTO source, Company targetCompany)
        {
            if (source.JobMetrics == null)
            {
                silverContext.CompanyJobMetrics.RemoveRange(targetCompany.CompanyJobMetrics);
            }
            else
            {
                var metricSource = source.JobMetrics;

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

                var properties = PropertyMetadataStorage.CurrentPropertyMetadata[typeof(CompanyJobMetrics).Name];
                foreach (var property in properties.Values)
                {
                    var result = (property.PropertyName.ToLower() switch
                    {
                        "averagesalary" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.AverageSalary, x => target.AverageSalary = x),
                        "employtraining" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.EmployTraining, x => target.EmployTraining = x),
                        "employturnoverfired" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.EmployTurnoverFired, x => target.EmployTurnoverFired = x),
                        "employturnovertotal" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.EmployTurnoverTotal, x => target.EmployTurnoverTotal = x),
                        "employturnovervoluntary" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.EmployTurnoverVoluntary, x => target.EmployTurnoverVoluntary = x),
                        "jobtenureaverage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.JobTenureAverage, x => target.JobTenureAverage = x),
                        "mediansalary" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.MedianSalary, x => target.MedianSalary = x),
                        "totalhours" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.TotalHours, x => target.TotalHours = x),
                        _ => throw new NotSupportedException($"{property.EntityName} {property.PropertyName}"),
                    });
                }
            }
        }

        private void FillCompanyHealthMetrics(CompanyDTO source, Company targetCompany)
        {
            if (source.HealthMetrics == null)
            {
                silverContext.CompanyHealthMetrics.RemoveRange(targetCompany.CompanyHealthMetrics);
            }
            else
            {

                var metricSource = source.HealthMetrics;

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

                var properties = PropertyMetadataStorage.CurrentPropertyMetadata[typeof(CompanyHealthMetrics).Name];
                foreach (var property in properties.Values)
                {
                    var result = (property.PropertyName.ToLower() switch
                    {
                        "ageaverage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.AgeAverage, x => target.AgeAverage = x),
                        "fatalities" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.Fatalities, x => target.Fatalities = x),
                        "healthtri" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.HealthTRI, x => target.HealthTRI = x),
                        "healthtrir" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.HealthTRIR, x => target.HealthTRIR = x),
                        "sickabsence" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.SickAbsence, x => target.SickAbsence = x),
                        _ => throw new NotSupportedException($"{property.EntityName} {property.PropertyName}"),
                    });
                }
            }
        }

        private void FillCompanyBoardStatistics(CompanyDTO source, Company targetCompany)
        {
            if (source.BoardStatistics == null)
            {
                targetCompany.CompanyBoardStatistics.Clear();
            }
            else
            {
                var metricSource = source.BoardStatistics;

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

                var properties = PropertyMetadataStorage.CurrentPropertyMetadata[typeof(CompanyBoardStatistics).Name];
                foreach (var property in properties.Values)
                {
                    var result = (property.PropertyName.ToLower() switch
                    {
                        "arabpercentage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.ArabPercentage, x => target.ArabPercentage = x),
                        "asianpercentage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.AsianPercentage, x => target.AsianPercentage = x),
                        "averageage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.AverageAge, x => target.AverageAge = x),
                        "averageeducationlength" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.AverageEducationLength, x => target.AverageEducationLength = x),
                        "blackpercentage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.BlackPercentage, x => target.BlackPercentage = x),
                        "caucasianpercentage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.CaucasianPercentage, x => target.CaucasianPercentage = x),
                        "femaleratio" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.FemaleRatio, x => target.FemaleRatio = x),
                        "height" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.Height, x => target.Height = x),
                        "hispanicpercentage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.HispanicPercentage, x => target.HispanicPercentage = x),
                        "indigenouspercentage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.IndigenousPercentage, x => target.IndigenousPercentage = x),
                        "membersnumber" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.MembersNumber, x => target.MembersNumber = x),
                        "salaryaverage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.SalaryAverage, x => target.SalaryAverage = x),
                        "salarymean" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.SalaryMean, x => target.SalaryMean = x),
                        "weight" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.Weight, x => target.Weight = x),
                        _ => throw new NotSupportedException($"{property.EntityName} {property.PropertyName}"),
                    });
                }
            }
        }

        private void FillCompanyExecutiveStatistics(CompanyDTO source, Company targetCompany)
        {
            if (source.ExecutiveStatistics == null)
            {
                targetCompany.CompanyExecutiveStatistics.Clear();
            }
            else
            {
                var metricSource = source.ExecutiveStatistics;

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

                var properties = PropertyMetadataStorage.CurrentPropertyMetadata[typeof(CompanyExecutiveStatistics).Name];
                foreach (var property in properties.Values)
                {
                    var result = (property.PropertyName.ToLower() switch
                    {
                        "arabpercentage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.ArabPercentage, x => target.ArabPercentage = x),
                        "asianpercentage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.AsianPercentage, x => target.AsianPercentage = x),
                        "averageage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.AverageAge, x => target.AverageAge = x),
                        "averageeducationlength" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.AverageEducationLength, x => target.AverageEducationLength = x),
                        "blackpercentage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.BlackPercentage, x => target.BlackPercentage = x),
                        "caucasianpercentage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.CaucasianPercentage, x => target.CaucasianPercentage = x),
                        "femaleratio" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.FemaleRatio, x => target.FemaleRatio = x),
                        "height" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.Height, x => target.Height = x),
                        "hispanicpercentage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.HispanicPercentage, x => target.HispanicPercentage = x),
                        "indigenouspercentage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.IndigenousPercentage, x => target.IndigenousPercentage = x),
                        "membersnumber" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.MembersNumber, x => target.MembersNumber = x),
                        "salaryaverage" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.SalaryAverage, x => target.SalaryAverage = x),
                        "salarymean" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.SalaryMean, x => target.SalaryMean = x),
                        "weight" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.Weight, x => target.Weight = x),
                        _ => throw new NotSupportedException($"{property.EntityName} {property.PropertyName}"),
                    });
                }
            }
        }

        private void FillCompanyKeyFinancialsMetrics(CompanyDTO source, Company targetCompany)
        {
            if (source.KeyFinancialsMetrics == null)
            {
                silverContext.CompanyKeyFinancialsMetrics.RemoveRange(targetCompany.CompanyKeyFinancialsMetrics);
            }
            else
            {

                var metricSource = source.KeyFinancialsMetrics;

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
                var properties = PropertyMetadataStorage.CurrentPropertyMetadata[typeof(CompanyKeyFinancialsMetrics).Name];
                foreach (var property in properties.Values)
                {
                    var result = (property.PropertyName.ToLower() switch
                    {
                        "employees" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.Employees, x => target.Employees = x),
                        "operatingrevenue" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.OperatingRevenue, x => target.OperatingRevenue = x),
                        "totalassets" => ValidationHelper.ValidateAndSetProperty(property, () => metricSource.TotalAssets, x => target.TotalAssets = x),
                        _ => throw new NotSupportedException($"{property.EntityName} {property.PropertyName}"),
                    });
                }
            }
        }

        private void FillCompanyCountries(CompanyDTO source, Company targetCompany)
        {
            // countries
            var srcIds = source.Countries
                .Select(x => x.Country)
                .ToHashSet();

            var toRemove = targetCompany.CompanyCountries.Where(x => !srcIds.Contains(x.CountryId)).ToList();

            foreach (var item in toRemove)
            {
                targetCompany.CompanyCountries.Remove(item);
                silverContext.Remove(item);
            }

            var targetCompanyCountries = targetCompany.CompanyCountries.ToDictionary(x => x.CountryId, x => x);

            foreach (var companyCountry in source.Countries)
            {
                CompanyCountry targetEntity;

                if (!targetCompanyCountries.ContainsKey(companyCountry.Country))
                {
                    targetEntity = new CompanyCountry()
                    {
                        Company = targetCompany,
                        CountryId = companyCountry.Country,
                    };

                    targetCompany.CompanyCountries.Add(targetEntity);
                    silverContext.CompanyCountries.Add(targetEntity);
                }
                else
                {
                    targetEntity = targetCompanyCountries[companyCountry.Country];
                }

                var properties = PropertyMetadataStorage.CurrentPropertyMetadata[typeof(CompanyCountry).Name];
                if(properties.ContainsKey("ticker"))
                {
                    ValidationHelper.ValidateAndSetProperty(properties["ticker"], () => companyCountry.Ticker, x => targetEntity.Ticker = x);
                }
            }
        }

        private void FillCompanyIndustries(CompanyDTO source, Company targetCompany)
        {
            // industries
            var industries = silverContext.Industries.Select(x => x.Id).ToHashSet();
            var industryCodes = silverContext.Industries.Select(x => x.Id).ToHashSet();

            var srcIds = source.Industries
                .Select(x => x.Industry.Value)
                .ToHashSet();

            var toRemove = targetCompany.CompanyIndustries.Where(x => !srcIds.Contains((int)x.Industry)).ToList();

            foreach (var item in toRemove)
            {
                targetCompany.CompanyIndustries.Remove(item);
                silverContext.Remove(item);
            }

            var targetCompanyIndustries = targetCompany.CompanyIndustries.ToDictionary(x => (int)x.Industry, x => x);

            foreach (var companyIndustry in source.Industries)
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

                var properties = PropertyMetadataStorage.CurrentPropertyMetadata[typeof(CompanyIndustry).Name];
                foreach(var property in properties.Values)
                {
                    var result = (property.PropertyName.ToLower() switch
                    {
                        "industry" => true,
                        "industrycode" => ValidationHelper.ValidateAndSetProperty(property, () => companyIndustry.IndustryCode, x => targetEntity.IndustryCode = (IndustryCode)x.Value),
                        "isprimary" => ValidationHelper.ValidateAndSetProperty(property, () => companyIndustry.IsPrimary, x => targetEntity.IsPrimary = x),
                        "tradedescription" => ValidationHelper.ValidateAndSetProperty(property, () => companyIndustry.TradeDescription, x => targetEntity.TradeDescription = x),
                        _ => throw new NotSupportedException($"{property.EntityName} {property.PropertyName}"),
                    });
                }
            }
        }

        private void FillCompanyNames(CompanyDTO source, Company targetCompany)
        {
            // names

            var properties = PropertyMetadataStorage.CurrentPropertyMetadata[typeof(CompanyName).Name];
            if (!(properties.ContainsKey("name") && properties["name"].IsEditable
                && properties.ContainsKey("nametype") && properties["nametype"].IsEditable))
                return;

            if (source.Names.Any(
                x => x == null
                || string.IsNullOrEmpty(x.NameType)
                || string.IsNullOrEmpty(x.Name)))
            {
                throw new ArgumentException("company names");
            }

            var toRemove = targetCompany.CompanyNames.Where(x => !source.Names.Any(y=>y.Name == x.Name && x.NameType == y.NameType)).ToList();

            foreach (var item in toRemove)
            {
                targetCompany.CompanyNames.Remove(item);
                silverContext.Remove(item);
            }

            var toAdd = source.Names.Where(x => !targetCompany.CompanyNames.Any(y => y.Name == x.Name && x.NameType == y.NameType)).ToList();

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

                var properties = PropertyMetadataStorage.CurrentPropertyMetadata[typeof(Role).Name];
                foreach (var property in properties.Values)
                {
                    var result = (property.PropertyName.ToLower() switch
                    {
                        "roletype" => true,
                        "title" => true,
                        "BaseSalary" => ValidationHelper.ValidateAndSetProperty(property, () => role.BaseSalary, x => targetEntity.BaseSalary = x),
                        "jobtenure" => ValidationHelper.ValidateAndSetProperty(property, () => role.JobTenure, x => targetEntity.JobTenure = x),
                        "otherincentives" => ValidationHelper.ValidateAndSetProperty(property, () => role.OtherIncentives, x => targetEntity.OtherIncentives = x),
                        _ => throw new NotSupportedException($"{property.EntityName} {property.PropertyName}"),
                    });
                }
            }
        }
    }
}
