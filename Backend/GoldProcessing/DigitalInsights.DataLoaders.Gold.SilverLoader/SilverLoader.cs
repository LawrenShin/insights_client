using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using DigitalInsights.Common.Logging;
using DigitalInsights.DB.Gold;
using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.DB.Gold.Entities.CompanyData;
using DigitalInsights.DB.Silver;
using Microsoft.EntityFrameworkCore;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DigitalInsights.DataLoaders.Gold.SilverLoader
{
    public class SilverLoader
    {
        const string UPDATER_NAME = "SilverLoader";

        const int PAGE_SIZE = 10000;

        const int THREAD_COUNT = 24;

        volatile int currentPage = -1;

        Dictionary<int, int> countryMap = new Dictionary<int, int>();

        /// <summary>
        /// An uploader from Silver to Gold
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public void FunctionHandler(ILambdaContext context)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Logger.Init(UPDATER_NAME);
                Logger.Log("Started");

                // 1) Clean up Gold
                var goldContext = new GoldContext();
                goldContext.ChangeTracker.AutoDetectChangesEnabled = false;
                goldContext.ChangeTracker.LazyLoadingEnabled = true;
                goldContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                Logger.Log("Cleaning up Gold");

                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryDemographics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryEconomicPowers CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryLaborForces CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryGenders CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryAges CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryReligions CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryEducation CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryRaces CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountrySexualities CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryDisabilities CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryUrbanization CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryPolitical CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryEconomicEqualities CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryUtilities  CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryInfrastructures CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryLaborAndSocialProtection CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryPrivateSectorsAndTrades CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryPublicSectors CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CountryIndustries CASCADE");

                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE Companies CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE Addresses CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyIdentities CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyNames CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyUrbanizationMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyHealthMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanySexualityMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyNationalityMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyFamilyMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanySentimentScoreMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyHierarchyMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyJobMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyKeyFinancialsMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyOwnershipMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyPoliticalMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyGenderMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyRaceMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyDisabilityMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyEducationMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyDIMetrics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyCountries CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyLegalInformation CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyIndustries CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyExecutiveStatistics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CompanyBoardStatistics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE People CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE PersonNationalities CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE Ratings CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE Roles CASCADE");

                Logger.Log("Gold cleaned up");

                // 2) Load industry and country data

                var silverContext = new SilverContext();
                silverContext.ChangeTracker.AutoDetectChangesEnabled = false;
                silverContext.ChangeTracker.LazyLoadingEnabled = true;
                silverContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                LoadCountries(silverContext, goldContext);

                LoadIndustries(silverContext, goldContext);

                // 3) Loading companies and people

                LoadCompanies();

                sw.Stop();
                Logger.Log($"Finished successfully in {sw.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Logger.Log($"ERROR: Unhandled exception: {ex.ToString()}");
            }
        }

        private void LoadCompanies()
        {
            Semaphore s = new Semaphore(0, THREAD_COUNT);

            ThreadStart loadCompaniesChunk = () =>
            {
                try
                {
                    int page = 0;
                    while (true)
                    {
                        page = Interlocked.Increment(ref currentPage);

                        var silverContext = new SilverContext();
                        silverContext.ChangeTracker.AutoDetectChangesEnabled = false;
                        silverContext.ChangeTracker.LazyLoadingEnabled = true;
                        silverContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                        var goldContext = new GoldContext();
                        goldContext.ChangeTracker.AutoDetectChangesEnabled = false;
                        goldContext.ChangeTracker.LazyLoadingEnabled = true;
                        goldContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                        Logger.Log($"Statrting to load page {page} of companies");

                        var companies = silverContext.Companies.AsNoTracking()
                            .Include(x => x.CompanyAddresses)
                            .Include(x => x.CompanyBoardStatistics)
                            .Include(x => x.CompanyCountries)
                            .Include(x => x.CompanyDIMetrics)
                            .Include(x => x.CompanyDisabilityMetrics)
                            .Include(x => x.CompanyEducationMetrics)
                            .Include(x => x.CompanyExecutiveStatistics)
                            .Include(x => x.CompanyFamilyMetrics)
                            .Include(x => x.CompanyGenderMetrics)
                            .Include(x => x.CompanyHealthMetrics)
                            .Include(x => x.CompanyHierarchyMetrics)
                            .Include(x => x.CompanyIdentities)
                            .Include(x => x.CompanyIndustries)
                            .Include(x => x.CompanyJobMetrics)
                            .Include(x => x.CompanyKeyFinancialsMetrics)
                            .Include(x => x.CompanyLegalInformations)
                            .Include(x => x.CompanyNames)
                            .Include(x => x.CompanyNationalityMetrics)
                            .Include(x => x.CompanyOwnershipMetrics)
                            .Include(x => x.CompanyPoliticalMetrics)
                            .Include(x => x.CompanyRaceMetrics)
                            .Include(x => x.CompanyReligionMetrics)
                            .Include(x => x.CompanySentimentScoreMetrics)
                            .Include(x => x.CompanySexualityMetrics)
                            .Include(x => x.CompanyUrbanizationMetrics)
                            .Include(x => x.Roles).ThenInclude(x => x.Person).ThenInclude(x => x.PersonNationalities)
                            .Skip(currentPage * PAGE_SIZE).Take(PAGE_SIZE).ToList();

                        if (companies.Count == 0) break;

                        var companiesMap = new Dictionary<DB.Silver.Entities.Company, Company>();

                        foreach (var company in companies)
                        {
                            var newCompany = new Company()
                            {
                                LegalName = company.LegalName,
                                LEI = company.LEI,
                            };

                            companiesMap[company] = newCompany;
                            goldContext.Companies.Add(newCompany);
                        }

                        var people = companies.SelectMany(x => x.Roles.Select(x => x.Person)).ToList();

                        var peopleMap = new Dictionary<DB.Silver.Entities.Person, Person>();

                        foreach (var person in people)
                        {
                            var newPerson = new Person()
                            {
                                Age = person.Age,
                                EducationInstitute = person.EducationInstitute,
                                EducationSubject = person.EducationSubject,
                                Gender = person.Gender,
                                HighEducation = person.HighEducation,
                                Kids = person.Kids,
                                Married = person.Married,
                                Name = person.Name,
                                Race = person.Race,
                                RandomName = person.RandomName,
                                Religion = person.Religion,
                                Sexuality = person.Sexuality,
                                Urban = person.Urban,
                                VisibleDisability = person.VisibleDisability,
                            };

                            peopleMap[person] = newPerson;
                            goldContext.People.Add(newPerson);
                        }

                        goldContext.SaveChanges();

                        foreach (var personNationality in people.SelectMany(x => x.PersonNationalities).ToList())
                        {
                            var newPersonNationality = new PersonNationality()
                            {
                                CountryId = personNationality.CountryId,
                                PersonId = peopleMap[personNationality.Person].Id,
                            };

                            goldContext.PersonNationalities.Add(newPersonNationality);
                        }

                        foreach (var companyAddress in companies.SelectMany(x => x.CompanyAddresses).ToList())
                        {
                            var newCompanyAddress = new Address()
                            {
                                CompanyId= companiesMap[companyAddress.Company].Id,
                                AddressType = companyAddress.AddressType,
                                City = companyAddress.City,
                                IsEditable = companyAddress.IsEditable,
                                PostCode = companyAddress.PostCode,
                                State = companyAddress.State,
                                StreetOne = companyAddress.StreetOne,
                                StreetTwo = companyAddress.StreetTwo,
                            };

                            goldContext.Addresses.Add(newCompanyAddress);
                        }

                        foreach (var companyBoardStatistics in companies.SelectMany(x => x.CompanyBoardStatistics).ToList())
                        {
                            var newCompanyBoardStatistics = new CompanyBoardStatistics()
                            {
                                CompanyId = companiesMap[companyBoardStatistics.Company].Id,
                                ArabPercentage = companyBoardStatistics.ArabPercentage,
                                AsianPercentage = companyBoardStatistics.AsianPercentage,
                                AverageAge = companyBoardStatistics.AverageAge,
                                AverageEducationLength = companyBoardStatistics.AverageEducationLength,
                                BlackPercentage = companyBoardStatistics.BlackPercentage,
                                CaucasianPercentage = companyBoardStatistics.CaucasianPercentage,
                                FemaleRatio = companyBoardStatistics.FemaleRatio,
                                Height = companyBoardStatistics.Height,
                                HispanicPercentage = companyBoardStatistics.HispanicPercentage,
                                IndigenousPercentage = companyBoardStatistics.IndigenousPercentage,
                                MembersNumber = companyBoardStatistics.MembersNumber,
                                SalaryAverage = companyBoardStatistics.SalaryAverage,
                                SalaryMean = companyBoardStatistics.SalaryMean,
                                Weight = companyBoardStatistics.Weight,
                            };

                            goldContext.CompanyBoardStatistics.Add(newCompanyBoardStatistics);
                        }

                        foreach (var companyExecutiveStatistics in companies.SelectMany(x => x.CompanyExecutiveStatistics).ToList())
                        {
                            var newCompanyExecutiveStatistics = new CompanyExecutiveStatistics()
                            {
                                CompanyId = companiesMap[companyExecutiveStatistics.Company].Id,
                                ArabPercentage = companyExecutiveStatistics.ArabPercentage,
                                AsianPercentage = companyExecutiveStatistics.AsianPercentage,
                                AverageAge = companyExecutiveStatistics.AverageAge,
                                AverageEducationLength = companyExecutiveStatistics.AverageEducationLength,
                                BlackPercentage = companyExecutiveStatistics.BlackPercentage,
                                CaucasianPercentage = companyExecutiveStatistics.CaucasianPercentage,
                                FemaleRatio = companyExecutiveStatistics.FemaleRatio,
                                Height = companyExecutiveStatistics.Height,
                                HispanicPercentage = companyExecutiveStatistics.HispanicPercentage,
                                IndigenousPercentage = companyExecutiveStatistics.IndigenousPercentage,
                                MembersNumber = companyExecutiveStatistics.MembersNumber,
                                SalaryAverage = companyExecutiveStatistics.SalaryAverage,
                                SalaryMean = companyExecutiveStatistics.SalaryMean,
                                Weight = companyExecutiveStatistics.Weight,
                            };

                            goldContext.CompanyExecutiveStatistics.Add(newCompanyExecutiveStatistics);
                        }

                        foreach (var companyCountry in companies.SelectMany(x => x.CompanyCountries).ToList())
                        {
                            var newCompanyCountry = new CompanyCountry()
                            {
                                CompanyId = companiesMap[companyCountry.Company].Id,
                                CountryId = companyCountry.CountryId,
                                Ticker = companyCountry.Ticker,
                            };

                            goldContext.CompanyCountries.Add(newCompanyCountry);
                        }

                        foreach (var companyDIMetrics in companies.SelectMany(x => x.CompanyDIMetrics).ToList())
                        {
                            var newCompanyDIMetrics = new CompanyDIMetrics()
                            {
                                CompanyId = companiesMap[companyDIMetrics.Company].Id,
                                DICodeConduct = companyDIMetrics.DICodeConduct,
                                DIComplaint = companyDIMetrics.DIComplaint,
                                DIDivision = companyDIMetrics.DIDivision,
                                DIEarningCall = companyDIMetrics.DIEarningCall,
                                DIFTEPosition = companyDIMetrics.DIFTEPosition,
                                DIPolicyEstablished = companyDIMetrics.DIPolicyEstablished,
                                DIPosition = companyDIMetrics.DIPosition,
                                DIPositionExecutive = companyDIMetrics.DIPositionExecutive,
                                DIPublicAvailable = companyDIMetrics.DIPublicAvailable,
                                DISupplyChain = companyDIMetrics.DISupplyChain,
                                DISupplySpendRevenueRatio = companyDIMetrics.DISupplySpendRevenueRatio,
                                DITalentGoals = companyDIMetrics.DITalentGoals,
                                DIWebsite = companyDIMetrics.DIWebsite,
                                EmployEngagement = companyDIMetrics.EmployEngagement,
                                EmploySatisfactionSurvey = companyDIMetrics.EmploySatisfactionSurvey,
                                EmploySurveyResponseRate = companyDIMetrics.EmploySurveyResponseRate,
                                HarassmentPolicy = companyDIMetrics.HarassmentPolicy,
                                HolidaySupport = companyDIMetrics.HolidaySupport,
                                ManagingDiverse = companyDIMetrics.ManagingDiverse,
                                MentorProgram = companyDIMetrics.MentorProgram,
                                Retaliation = companyDIMetrics.Retaliation,
                                SocialEvents = companyDIMetrics.SocialEvents,
                                SocialProgram = companyDIMetrics.SocialProgram,
                                SupplySpend = companyDIMetrics.SupplySpend,
                                ValueDISupplySpend = companyDIMetrics.ValueDISupplySpend
                            };

                            foreach (var companyDisabilityMetrics in companies.SelectMany(x => x.CompanyDisabilityMetrics).ToList())
                            {
                                var newCompanyDisabilityMetrics = new CompanyDisabilityMetrics()
                                {
                                    CompanyId = companiesMap[companyDisabilityMetrics.Company].Id,
                                    DisabelMental = companyDisabilityMetrics.DisabelMental,
                                    DisabelPhysical= companyDisabilityMetrics.DisabelPhysical,
                                    DisabelProgram = companyDisabilityMetrics.DisabelProgram,
                                    DisabelTotal = companyDisabilityMetrics.DisabelTotal,
                                    WheelchairAccess = companyDisabilityMetrics.WheelchairAccess,
                                };

                                goldContext.CompanyDisabilityMetrics.Add(newCompanyDisabilityMetrics);
                            }

                            foreach (var ñompanyEducationMetrics in companies.SelectMany(x => x.CompanyEducationMetrics).ToList())
                            {
                                var newCompanyEducationMetrics = new CompanyEducationMetrics()
                                {
                                    CompanyId = companiesMap[ñompanyEducationMetrics.Company].Id,
                                    BachelorShare = ñompanyEducationMetrics.BachelorShare,
                                    EducationLeaveSupport = ñompanyEducationMetrics.EducationLeaveSupport,
                                    EducationSupportProgram = ñompanyEducationMetrics.EducationSupportProgram,
                                    ElementaryShare = ñompanyEducationMetrics.ElementaryShare,
                                    HighschoolShare = ñompanyEducationMetrics.HighschoolShare,
                                    MasterShare = ñompanyEducationMetrics.MasterShare,
                                    StudentDebt = ñompanyEducationMetrics.StudentDebt,
                                };

                                goldContext.CompanyEducationMetrics.Add(newCompanyEducationMetrics);
                            }

                            foreach (var companyFamilyMetrics in companies.SelectMany(x => x.CompanyFamilyMetrics).ToList())
                            {
                                var newCompanyFamilyMetrics = new CompanyFamilyMetrics()
                                {
                                    CompanyId = companiesMap[companyFamilyMetrics.Company].Id,
                                    ParentalGender = companyFamilyMetrics.ParentalGender,
                                    ParentalLeave = companyFamilyMetrics.ParentalLeave,
                                    ParentalTime = companyFamilyMetrics.ParentalTime,
                                };

                                goldContext.CompanyFamilyMetrics.Add(newCompanyFamilyMetrics);
                            }

                            foreach (var companyGenderMetrics in companies.SelectMany(x => x.CompanyGenderMetrics).ToList())
                            {
                                var newCompanyGenderMetrics = new CompanyGenderMetrics()
                                {
                                    CompanyId = companiesMap[companyGenderMetrics.Company].Id,
                                    GenderMale = companyGenderMetrics.GenderMale,
                                    GenderOther = companyGenderMetrics.GenderOther,
                                    GenderPayGap = companyGenderMetrics.GenderPayGap,
                                    GenderRatioAll = companyGenderMetrics.GenderRatioAll,
                                    GenderRatioBoard = companyGenderMetrics.GenderRatioMiddle,
                                    GenderRatioMiddle = companyGenderMetrics.GenderRatioMiddle,
                                    GenderRatioSenior = companyGenderMetrics.GenderRatioSenior,
                                };

                                goldContext.CompanyGenderMetrics.Add(newCompanyGenderMetrics);
                            }

                            foreach (var companyHierarchyMetrics in companies.SelectMany(x => x.CompanyHierarchyMetrics).ToList())
                            {
                                var newCompanyHierarchyMetrics = new CompanyHierarchyMetrics()
                                {
                                    CompanyId = companiesMap[companyHierarchyMetrics.Company].Id,
                                    HierachyLevel = companyHierarchyMetrics.HierachyLevel,
                                    Intranet = companyHierarchyMetrics.Intranet,
                                    OrganizationalStructure = companyHierarchyMetrics.OrganizationalStructure,
                                    TownHalls = companyHierarchyMetrics.TownHalls,
                                };

                                goldContext.CompanyHierarchyMetrics.Add(newCompanyHierarchyMetrics);
                            }

                            foreach (var companyHealthMetrics in companies.SelectMany(x => x.CompanyHealthMetrics).ToList())
                            {
                                var newCompanyHealthMetrics = new CompanyHealthMetrics()
                                {
                                    CompanyId = companiesMap[companyHealthMetrics.Company].Id,
                                    AgeAverage = companyHealthMetrics.AgeAverage,
                                    Fatalities = companyHealthMetrics.Fatalities,
                                    HealthTRI = companyHealthMetrics.HealthTRI,
                                    HealthTRIR = companyHealthMetrics.HealthTRIR,
                                    SickAbsence = companyHealthMetrics.SickAbsence,
                                };

                                goldContext.CompanyHealthMetrics.Add(newCompanyHealthMetrics);
                            }

                            foreach (var companyIdentity in companies.SelectMany(x => x.CompanyIdentities).ToList())
                            {
                                var newCompanyIdentity = new CompanyIdentity()
                                {
                                    CompanyId = companiesMap[companyIdentity.Company].Id,
                                    ISIN = companyIdentity.ISIN,
                                    OtherLabel = companyIdentity.OtherLabel,
                                    OtherNumber = companyIdentity.OtherNumber,
                                    TaxId = companyIdentity.TaxId,
                                };

                                goldContext.CompanyIdentities.Add(newCompanyIdentity);
                            }

                            foreach (var source in companies.SelectMany(x => x.CompanyIndustries).ToList())
                            {
                                var target = new CompanyIndustry()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    Industry = source.Industry,
                                    IndustryCode = source.IndustryCode,
                                    IsPrimary = source.IsPrimary,
                                    TradeDescription = source.TradeDescription,
                                };

                                goldContext.CompanyIndustries.Add(target);
                            }

                            foreach (var source in companies.SelectMany(x => x.CompanyJobMetrics).ToList())
                            {
                                var target = new CompanyJobMetrics()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    AverageSalary = source.AverageSalary,
                                    EmployTraining = source.EmployTraining,
                                    EmployTurnoverFired = source.EmployTurnoverFired,
                                    EmployTurnoverTotal = source.EmployTurnoverTotal,
                                    EmployTurnoverVoluntary = source.EmployTurnoverVoluntary,
                                    JobTenureAverage = source.JobTenureAverage,
                                    MedianSalary = source.MedianSalary,
                                    TotalHours = source.TotalHours,
                                };

                                goldContext.CompanyJobMetrics.Add(target);
                            }

                            foreach (var source in companies.SelectMany(x => x.CompanyKeyFinancialsMetrics).ToList())
                            {
                                var target = new CompanyKeyFinancialsMetrics()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    Employees = source.Employees,
                                    OperatingRevenue = source.OperatingRevenue,
                                    TotalAssets = source.TotalAssets,
                                };

                                goldContext.CompanyKeyFinancialsMetrics.Add(target);
                            }

                            foreach (var source in companies.SelectMany(x => x.CompanyLegalInformations).ToList())
                            {
                                var target = new CompanyLegalInformation()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    CompanyIndex = source.CompanyIndex,
                                    CompanyPublic = source.CompanyPublic,
                                    IncorporationDate = source.IncorporationDate,
                                    LegalForm = source.LegalForm,
                                    Status = source.Status,
                                };

                                goldContext.CompanyLegalInformations.Add(target);
                            }

                            foreach (var source in companies.SelectMany(x => x.CompanyNames).ToList())
                            {
                                var target = new CompanyName()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    Name = source.Name,
                                    NameType = source.NameType,
                                };

                                goldContext.CompanyNames.Add(target);
                            }

                            foreach (var source in companies.SelectMany(x => x.CompanyNationalityMetrics).ToList())
                            {
                                var target = new CompanyNationalityMetrics()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    CultureERG = source.CultureERG,
                                    NationalDifferent = source.NationalDifferent,
                                    NationalNumberOpeRation = source.NationalNumberOpeRation,
                                    NationalTopFive = source.NationalTopFive,
                                    SupportLanguages = source.SupportLanguages,
                                };

                                goldContext.CompanyNationalityMetrics.Add(target);
                            }

                            foreach (var source in companies.SelectMany(x => x.CompanyOwnershipMetrics).ToList())
                            {
                                var target = new CompanyOwnershipMetrics()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    DisabledOwned25Percents = source.DisabledOwned25Percents,
                                    DisabledOwnedMajority = source.DisabledOwnedMajority,
                                    LGBTOwned25Percents = source.LGBTOwned25Percents,
                                    LGBTOwnedMajority = source.LGBTOwnedMajority,
                                    MinorityOwned25Percents = source.MinorityOwned25Percents,
                                    MinorityOwnedMajority = source.MinorityOwnedMajority,
                                    VeteranOwned25Percents = source.VeteranOwned25Percents,
                                    VetranOwnedMajority = source.VetranOwnedMajority,
                                    WomanOwned25Percents = source.WomanOwned25Percents,
                                    WomanOwnedMajority = source.WomanOwnedMajority,
                                };

                                goldContext.CompanyOwnershipMetrics.Add(target);
                            }

                            foreach (var source in companies.SelectMany(x => x.CompanyPoliticalMetrics).ToList())
                            {
                                var target = new CompanyPoliticalMetrics()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    NonDiscriminationPolitical = source.NonDiscriminationPolitical,
                                    PoliticalVote = source.PoliticalVote,
                                    SupportPolitical = source.SupportPolitical,
                                };

                                goldContext.CompanyPoliticalMetrics.Add(target);
                            }

                            foreach (var source in companies.SelectMany(x => x.CompanyRaceMetrics).ToList())
                            {
                                var target = new CompanyRaceMetrics()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    RaceArab = source.RaceArab,
                                    RaceAsian = source.RaceAsian,
                                    RaceBlack = source.RaceBlack,
                                    RaceCaucasian = source.RaceCaucasian,
                                    RaceHispanic = source.RaceHispanic,
                                    RaceIndigenous = source.RaceIndigenous,
                                    RaceRatioAll = source.RaceRatioAll,
                                    RaceRatioBoard = source.RaceRatioBoard,
                                    RaceRatioExececutive = source.RaceRatioExececutive,
                                    RaceRatioMiddle = source.RaceRatioMiddle,
                                    RaceRatioSenior = source.RaceRatioSenior,
                                };

                                goldContext.CompanyRaceMetrics.Add(target);
                            }

                            foreach (var source in companies.SelectMany(x => x.CompanyReligionMetrics).ToList())
                            {
                                var target = new CompanyReligionMetrics()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    BuddhismShare = source.BuddhismShare,
                                    ChristianShare = source.ChristianShare,
                                    HinduShare = source.HinduShare,
                                    HolidayReligion = source.HolidayReligion,
                                    JudaismShare = source.JudaismShare,
                                    MuslimShare = source.MuslimShare,
                                    NonDiscriminationReligion = source.NonDiscriminationReligion,
                                    OtherShare = source.OtherShare,
                                    PrayerRoom = source.PrayerRoom,
                                };

                                goldContext.CompanyReligionMetrics.Add(target);
                            }

                            foreach (var source in companies.SelectMany(x => x.CompanySentimentScoreMetrics).ToList())
                            {
                                var target = new CompanySentimentScoreMetrics()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    SentimentNegative = source.SentimentNegative,
                                    SentimentPositive = source.SentimentPositive,
                                };

                                goldContext.CompanySentimentScoreMetrics.Add(target);
                            }

                            foreach (var source in companies.SelectMany(x => x.CompanySexualityMetrics).ToList())
                            {
                                var target = new CompanySexualityMetrics()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    LBGTQForum = source.LBGTQForum,
                                    NonDiscriminationSexuality = source.NonDiscriminationSexuality,
                                    SexualityData = source.SexualityData,
                                    SupportDifferentSexuality = source.SupportDifferentSexuality,
                                };

                                goldContext.CompanySexualityMetrics.Add(target);
                            }

                            foreach (var source in companies.SelectMany(x => x.CompanyUrbanizationMetrics).ToList())
                            {
                                var target = new CompanyUrbanizationMetrics()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    RuralSites = source.RuralSites,
                                    UrbanSites = source.UrbanSites,
                                };

                                goldContext.CompanyUrbanizationMetrics.Add(target);
                            }

                            foreach (var source in companies.SelectMany(x => x.Roles).ToList())
                            {
                                var target = new Role()
                                {
                                    CompanyId = companiesMap[source.Company].Id,
                                    BaseSalary = source.BaseSalary,
                                    JobTenure = source.JobTenure,
                                    OtherIncentives = source.OtherIncentives,
                                    RoleType = source.RoleType,
                                    Title = source.Title,
                                    PersonId = peopleMap[source.Person].Id,
                                };

                                goldContext.Roles.Add(target);
                            }
                        }

                        goldContext.SaveChanges();
                        goldContext.Dispose();
                    }
                    Logger.Log($"No data for page {page}, finishing loading thread");
                }
                catch (Exception ex)
                {
                    Logger.Log($"ERROR: Unhandled exception: {ex.ToString()}");
                }
                finally
                {
                    s.Release();
                }
            };

            for (int i = 0; i < THREAD_COUNT; i++)
            {
                new Thread(loadCompaniesChunk).Start();
            }

            for (int i = 0; i < THREAD_COUNT; i++)
            {
                s.WaitOne();
            }
        }

        private void LoadIndustries(SilverContext silverContext, GoldContext goldContext)
        {
            Logger.Log("Loading industries");
            foreach (var countryIndustry in silverContext.CountryIndustries.ToList())
            {
                goldContext.CountryIndustries.Add(
                        new DB.Gold.Entities.CountryData.CountryIndustry()
                        {
                            Industry = countryIndustry.Industry,
                            Age = countryIndustry.Age,
                            AveragePay = countryIndustry.AveragePay,
                            CountryId = countryIndustry.CountryId,
                            Disabled = countryIndustry.Disabled,
                            EducationSpend = countryIndustry.EducationSpend,
                            Employees = countryIndustry.Employees,
                            FlexibleHours = countryIndustry.FlexibleHours,
                            Gender = countryIndustry.Gender,
                            Harassment = countryIndustry.Harassment,
                            InjuriesFatal = countryIndustry.InjuriesFatal,
                            InjuriesNonFatal = countryIndustry.InjuriesNonFatal,
                            LBGT = countryIndustry.LBGT,
                            Maternity = countryIndustry.Maternity,
                            Paternity = countryIndustry.Paternity,
                            Race = countryIndustry.Race,
                            Retention = countryIndustry.Retention,
                        });
            }

            goldContext.SaveChanges();

            Logger.Log("Industries loaded");
        }

        private void LoadCountries(SilverContext silverContext, GoldContext goldContext)
        {;
            Logger.Log("Loading countries");
            foreach (var country in silverContext.Countries.AsQueryable()
                    .Include(x => x.CountryAges)
                    .Include(x => x.CountryDemographics)
                    .Include(x => x.CountryDisabilities)
                    .Include(x => x.CountryEconomicPowers)
                    .Include(x => x.CountryEconomicEqualities)
                    .Include(x => x.CountryEducations)
                    .Include(x => x.CountryGenders)
                    .Include(x => x.CountryInfrastructures)
                    .Include(x => x.CountryLaborAndSocialProtections)
                    .Include(x => x.CountryLaborForces)
                    .Include(x => x.CountryPoliticals)
                    .Include(x => x.CountryPrivateSectorsAndTrades)
                    .Include(x => x.CountryPublicSectors)
                    .Include(x => x.CountryRaces)
                    .Include(x => x.CountryReligions)
                    .Include(x => x.CountrySexualities)
                    .Include(x => x.CountryUrbanizations)
                    .Include(x => x.CountryUtilities))
            {

                if (country.CountryAges.Count > 0)
                {
                    var countryAge = country.CountryAges.First();
                    goldContext.CountryAges.Add(new DB.Gold.Entities.CountryData.CountryAge()
                        {
                            CountryId = country.Id,
                            AgeAverage18 = countryAge.AgeAverage18,
                            AgeDistribution19 = countryAge.AgeDistribution19,
                            AgeDistribution39= countryAge.AgeDistribution39,
                            AgeDistribution59 = countryAge.AgeDistribution59,
                            AgeDistribution79 = countryAge.AgeDistribution79,
                            AgeDistributionx = countryAge.AgeDistributionx,
                            AgeMinisters = countryAge.AgeMinisters,
                            AgeParliament = countryAge.AgeParliament,
                        });
                }

                if (country.CountryDemographics.Count > 0)
                {
                    var countryDemographics = country.CountryDemographics.First();
                    goldContext.CountryDemographics.Add(
                        new DB.Gold.Entities.CountryData.CountryDemographics()
                        {
                            CountryId = country.Id,
                            ImmigrantPercentage = countryDemographics.ImmigrantPercentage,
                            ImmigrantPopulation = countryDemographics.ImmigrantPopulation,
                            Population = countryDemographics.Population,
                        });
                }

                if (country.CountryDisabilities.Count > 0)
                {
                    var countryDisability = country.CountryDisabilities.First();
                    goldContext.CountryDisabilities.Add(
                        new DB.Gold.Entities.CountryData.CountryDisability()
                        {
                            CountryId = country.Id,
                            Disabled = countryDisability.Disabled,
                            DisabilityDiscriminationLaw = countryDisability.DisabilityDiscriminationLaw,
                            HealthFundingGDP = countryDisability.HealthFundingGDP,
                            HealthType = countryDisability.HealthType,
                            Overweight = countryDisability.Overweight,
                        });
                }

                if (country.CountryEconomicPowers.Count > 0)
                {
                    var countryEconomy = country.CountryEconomicPowers.First();
                    goldContext.CountryEconomicPowers.Add(
                        new DB.Gold.Entities.CountryData.CountryEconomicPower()
                        {
                            CountryId = country.Id,
                            GDP = countryEconomy.GDP,
                            GDPPerCapita = countryEconomy.GDPPerCapita,
                            GDPWorld = countryEconomy.GDPWorld,
                        });
                }

                if (country.CountryEconomicEqualities.Count > 0)
                {
                    var countryEconomy = country.CountryEconomicEqualities.First();
                    goldContext.CountryEconomicEqualities.Add(
                        new DB.Gold.Entities.CountryData.CountryEconomicEquality()
                        {
                            CountryId = country.Id,
                            EqualityIndex = countryEconomy.EqualityIndex,
                            Poor = countryEconomy.Poor,
                        });
                }

                if (country.CountryEducations.Count > 0)
                {
                    var countryEdu = country.CountryEducations.First();
                    goldContext.CountryEducations.Add(
                        new DB.Gold.Entities.CountryData.CountryEducation()
                        {
                            CountryId = country.Id,
                            ActualEducation = countryEdu.ActualEducation,
                            BachelorFemale = countryEdu.BachelorFemale,
                            BachelorMale = countryEdu.BachelorMale,
                            BachelorMaleFemale= countryEdu.BachelorMaleFemale,
                            ElementaryFemale = countryEdu.ElementaryFemale,
                            ElementaryMale = countryEdu.ElementaryMale,
                            ElementaryMaleFemale = countryEdu.ElementaryMaleFemale,
                            ExpectedEducation = countryEdu.ExpectedEducation,
                            HighSchoolFemale = countryEdu.HighSchoolFemale,
                            HighSchoolMale = countryEdu.HighSchoolMale,
                            HighSchoolMaleFemale = countryEdu.HighSchoolMaleFemale,
                            MasterFemale = countryEdu.MasterFemale,
                            MasterMale = countryEdu.MasterMale,
                            MasterMaleFemale = countryEdu.MasterMaleFemale,
                            EducationPublicFundFund = countryEdu.EducationPublicFundFund,
                            EducationPublicFundingGDP = countryEdu.EducationPublicFundingGDP,
                            DoctoralFemale = countryEdu.DoctoralFemale,
                            DoctoralMale = countryEdu.DoctoralMale,
                            DoctoralMaleFemale = countryEdu.DoctoralMaleFemale,
                            FemaleLiteracy = countryEdu.FemaleLiteracy,
                            MaleLiteracy = countryEdu.MaleLiteracy,
                        });
                }

                if (country.CountryGenders.Count > 0)
                {
                    var countryGender = country.CountryGenders.First();
                    goldContext.CountryGenders.Add(
                        new DB.Gold.Entities.CountryData.CountryGender()
                        {
                            CountryId = country.Id,
                            FemaleMinisterShare = countryGender.FemaleMinisterShare,
                            FemaleParliamentShare = countryGender.FemaleParliamentShare,
                            FemalePopulationPercentage = countryGender.FemalePopulationPercentage,
                            FemalePromotionPolicy = countryGender.FemalePromotionPolicy,
                            FemaleWorkforce = countryGender.FemaleWorkforce,
                            FemaleWorkforcePercentage = countryGender.FemaleWorkforcePercentage,
                            FemaleWorkforcePopulationPercentage = countryGender.FemaleWorkforcePopulationPercentage,
                            GenderEducationcationGap = countryGender.GenderEducationcationGap,
                            GenderHealthGap = countryGender.GenderHealthGap,
                            GenderPoliticalGap = countryGender.GenderPoliticalGap,
                            GenderWorkGap = countryGender.GenderWorkGap,
                            IncomeGap = countryGender.IncomeGap,
                            LifeExpectancyFemale = countryGender.LifeExpectancyFemale,
                            LifeExpectancyMale = countryGender.LifeExpectancyMale,
                            MalePopulationPercentage = countryGender.MalePopulationPercentage,
                            Maternity = countryGender.Maternity,
                            Paternity = countryGender.Paternity,
                            WomenEducation = countryGender.WomenEducation,
                            WomenViolence = countryGender.WomenViolence,
                            EducatedFemaleUnemploy = countryGender.EducatedFemaleUnemploy,
                            EducatedMaleUnemploy = countryGender.EducatedMaleUnemploy,
                            FemaleSuicide = countryGender.FemaleSuicide,
                            FirmsFemaleManager = countryGender.FirmsFemaleManager,
                            FirmsFemaleOwnership = countryGender.FirmsFemaleOwnership,
                            MaleSuicide = countryGender.MaleSuicide
                        });
                }

                if (country.CountryInfrastructures.Count > 0)
                {
                    var countryInfrastructure = country.CountryInfrastructures.First();
                    goldContext.CountryInfrastructures.Add(
                        new DB.Gold.Entities.CountryData.CountryInfrastructure()
                        {
                            CountryId = country.Id,
                            CellularSubscriptions = countryInfrastructure.CellularSubscriptions,
                            InternetUse = countryInfrastructure.InternetUse,
                        });
                }

                if (country.CountryLaborAndSocialProtections.Count > 0)
                {
                    var countryLaborAndSocialProtection = country.CountryLaborAndSocialProtections.First();
                    goldContext.CountryLaborAndSocialProtections.Add(
                        new DB.Gold.Entities.CountryData.CountryLaborAndSocialProtection()
                        {
                            CountryId = country.Id,
                            FemaleManagementPercentage = countryLaborAndSocialProtection.FemaleManagementPercentage
                        });
                }

                if (country.CountryLaborForces.Count > 0)
                {
                    var countryLaborForce = country.CountryLaborForces.First();
                    goldContext.CountryLaborForces.Add(
                        new DB.Gold.Entities.CountryData.CountryLaborForce()
                        {
                            CountryId = country.Id,
                            AverageIncome = countryLaborForce.AverageIncome,
                            FemaleUnemployment = countryLaborForce.FemaleUnemployment,
                            LaborForce = countryLaborForce.LaborForce,
                            LaborForcePercentage = countryLaborForce.LaborForcePercentage,
                            MaleUnemployment = countryLaborForce.MaleUnemployment,
                        });
                }

                if (country.CountryPoliticals.Count > 0)
                {
                    var countryPolitical = country.CountryPoliticals.First();
                    goldContext.CountryPoliticals.Add(
                        new DB.Gold.Entities.CountryData.CountryPolitical()
                        {
                            CountryId = country.Id,
                            
                            CorruptionIndex = countryPolitical.CorruptionIndex,
                            DemocracyIndex = countryPolitical.DemocracyIndex,
                            FreeSpeechIndex = countryPolitical.FreeSpeechIndex,

                        });
                }

                if (country.CountryPrivateSectorsAndTrades.Count > 0)
                {
                    var countryPrivateSectorAndTrade = country.CountryPrivateSectorsAndTrades.First();
                    goldContext.CountryPrivateSectorsAndTrades.Add(
                        new DB.Gold.Entities.CountryData.CountryPrivateSectorAndTrade()
                        {
                            CountryId = country.Id,
                            CostOfBusiness = countryPrivateSectorAndTrade.CostOfBusiness,
                            EaseOfBusiness = countryPrivateSectorAndTrade.EaseOfBusiness,
                            FirmsBribery = countryPrivateSectorAndTrade.FirmsBribery,
                            FirmsTraining = countryPrivateSectorAndTrade.FirmsTraining,
                            StartupBusiness = countryPrivateSectorAndTrade.StartupBusiness,
                        });
                }

                if (country.CountryPublicSectors.Count > 0)
                {
                    var countryPublicSector = country.CountryPublicSectors.First();
                    goldContext.CountryPublicSectors.Add(
                        new DB.Gold.Entities.CountryData.CountryPublicSector()
                        {
                            CountryId = country.Id,
                            HumanCapital = countryPublicSector.HumanCapital,
                        });
                }

                if (country.CountryRaces.Count > 0)
                {
                    var countryRace = country.CountryRaces.First();
                    goldContext.CountryRaces.Add(
                        new DB.Gold.Entities.CountryData.CountryRace()
                        {
                            CountryId = country.Id,
                            Arab = countryRace.Arab,
                            Asian = countryRace.Asian,
                            Black = countryRace.Black,
                            Caucasian = countryRace.Caucasian,
                            RaceDiscriminationLaw = countryRace.RaceDiscriminationLaw,
                            Hispanic = countryRace.Hispanic,
                            Indegineous = countryRace.Indegineous,
                            CountryRaceHarassment = countryRace.CountryRaceHarassment,
                        });
                }

                if (country.CountryReligions.Count > 0)
                {
                    var countryReligion = country.CountryReligions.First();
                    goldContext.CountryReligions.Add(
                        new DB.Gold.Entities.CountryData.CountryReligion()
                        {
                            CountryId = country.Id,
                            Buddishm = countryReligion.Buddishm,
                            Christian = countryReligion.Christian,
                            ReligionFreedom = countryReligion.ReligionFreedom,
                            Hindu = countryReligion.Hindu,
                            Judaism = countryReligion.Judaism,
                            Muslim = countryReligion.Muslim,
                            StateReligion = countryReligion.StateReligion,
                            Other = countryReligion.Other,
                        });
                }

                if (country.CountrySexualities.Count > 0)
                {
                    var countrySexuality = country.CountrySexualities.First();
                    goldContext.CountrySexualities.Add(
                        new DB.Gold.Entities.CountryData.CountrySexuality()
                        {
                            CountryId = country.Id,
                            HomosexualPopulation = countrySexuality.HomosexualPopulation,
                            LGBTAdoption = countrySexuality.LGBTAdoption,
                            LGBTAntiLaws = countrySexuality.LGBTAntiLaws,
                            LGBTDeathSentences = countrySexuality.LGBTDeathSentences,
                            LGBTDiscriminationLaw = countrySexuality.LGBTDiscriminationLaw,
                            LGBTMarketing = countrySexuality.LGBTMarketing,
                            LGBTMarriage = countrySexuality.LGBTMarriage,
                            LGBTMurders = countrySexuality.LGBTMurders,
                            ConversionTherapy = countrySexuality.ConversionTherapy,
                            LGBTTolerance = countrySexuality.LGBTTolerance,
                            TransgenderRights = countrySexuality.TransgenderRights,
                        });
                }

                if (country.CountryUrbanizations.Count > 0)
                {
                    var countryUrbanization = country.CountryUrbanizations.First();
                    goldContext.CountryUrbanizations.Add(
                        new DB.Gold.Entities.CountryData.CountryUrbanization()
                        {
                            CountryId = country.Id,
                            LiveCities= countryUrbanization.LiveCities,

                        });
                }

                if (country.CountryUrbanizations.Count > 0)
                {
                    var countryUrbanization = country.CountryUrbanizations.First();
                    goldContext.CountryUrbanizations.Add(
                        new DB.Gold.Entities.CountryData.CountryUrbanization()
                        {
                            CountryId = country.Id,
                            LiveCities = countryUrbanization.LiveCities,

                        });
                }

                if (country.CountryUtilities.Count > 0)
                {
                    var countryUtility = country.CountryUtilities.First();
                    goldContext.CountryUtilities.Add(
                        new DB.Gold.Entities.CountryData.CountryUtility()
                        {
                            CountryId = country.Id,
                            AccessToDrinkingWater = countryUtility.AccessToDrinkingWater,
                            AccessToElectricity = countryUtility.AccessToElectricity,
                            AccessToHandWashing = countryUtility.AccessToHandWashing,
                            AccessToSanitation = countryUtility.AccessToSanitation,
                            SlumsPopulation = countryUtility.SlumsPopulation,
                        });
                }
            }

            goldContext.SaveChanges();

            Logger.Log("Countries loaded");
        }
    }
}
