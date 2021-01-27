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

                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE company_name CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE company_operation CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE role CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE person_country CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE person CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE industry_country CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE company_country CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE company_questionnaire CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE company CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE address CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE industry CASCADE");

                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE country_age CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE country_demographics CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE country_disability CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE country_economy CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE country_edu CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE country_gender CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE country_political CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE country_race CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE country_religion CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE country_sex CASCADE");
                goldContext.Database.ExecuteSqlRaw("TRUNCATE TABLE country_urban CASCADE");

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
                            .Include(x => x.Hq)
                            .Include(x => x.Legal)
                            .Include(x => x.CompanyNames)
                            .Include(x => x.CompanyQuestionnaires)
                            //.Include(x => x.CompanyOperation)
                            .Include(x => x.Roles).ThenInclude(x => x.Person).ThenInclude(x => x.PersonCountries)
                            .Skip(currentPage * PAGE_SIZE).Take(PAGE_SIZE).ToList();

                        if (companies.Count == 0) break;

                        var companiesToSave = new List<Company>();
                        var namesToSave = new List<CompanyName>();
                        var rolesToSave = new List<Role>();
                        var personsToSave = new List<Person>();
                        var countriesToSave = new List<PersonCountry>();

                        foreach (var company in companies)
                        {
                            var newCompany = new Company()
                            {
                                LegalJurisdiction = company.LegalJurisdiction,
                                LegalName = company.LegalName,
                                Lei = company.Lei,
                                NumEmployees = company.NumEmployees,
                                Status = company.Status,
                            };

                            var hqAddress = new Address()
                            {
                                AddressLine = company.Hq.AddressLine,
                                AddressNumber = company.Hq.AddressNumber,
                                City = company.Hq.City,
                                PostalCode = company.Hq.PostalCode,
                                Region = company.Hq.Region,
                            };

                            //hqAddress.CompanyHq.Add(newCompany);
                            newCompany.Hq = hqAddress;
                            goldContext.Add(hqAddress);

                            var legalAddress = new Address()
                            {
                                AddressLine = company.Legal.AddressLine,
                                AddressNumber = company.Legal.AddressNumber,
                                City = company.Legal.City,
                                PostalCode = company.Legal.PostalCode,
                                Region = company.Legal.Region,
                            };

                            //legalAddress.CompanyLegal.Add(newCompany);
                            newCompany.Legal = legalAddress;
                            goldContext.Add(legalAddress);

                            foreach (var companyName in company.CompanyNames)
                            {
                                var newCompanyName = new CompanyName()
                                {
                                    Company = newCompany,
                                    Name = companyName.Name,
                                    Type = companyName.Type,
                                };
                                newCompany.CompanyNames.Add(newCompanyName);
                                namesToSave.Add(newCompanyName);
                                //goldContext.Add(newCompanyName);
                            }

                            foreach (var companyQuestion in company.CompanyQuestionnaires)
                            {
                                var newCompanyQuestion = new CompanyQuestion()
                                {
                                    Company = newCompany,
                                    Question = (DB.Gold.Enums.CompanyQuestion)(int)companyQuestion.Question,
                                    Answer = companyQuestion.Answer
                                };
                                newCompany.CompanyQuestionnaires.Add(newCompanyQuestion);
                                goldContext.Add(newCompanyQuestion);
                            }

                            foreach (var role in company.Roles)
                            {
                                var newRole = new Role()
                                {
                                    Company = newCompany,
                                    BaseSalary = role.BaseSalary,
                                    IncentiveOptions = role.IncentiveOptions,
                                    RoleType = role.RoleType,
                                    Title = role.Title,
                                };

                                var person = role.Person;
                                var newPerson = new Person()
                                {
                                    BirthYear = person.BirthYear,
                                    EduInstitute = person.EduInstitute,
                                    EduSubject = person.EduSubject,
                                    Gender = person.Gender,
                                    HighEdu = person.HighEdu,
                                    Married = person.Married,
                                    Name = person.Name,
                                    Picture = person.Picture,
                                    Race = person.Race,
                                    Religion = person.Religion,
                                    Sexuality = person.Sexuality,
                                    BaseSalary = person.BaseSalary,
                                    OtherIncentive = person.OtherIncentive,
                                };

                                newPerson.Roles.Add(newRole);
                                newRole.Person = newPerson;
                                personsToSave.Add(newPerson);
                                //goldContext.Add(newPerson);

                                newCompany.Roles.Add(newRole);
                                rolesToSave.Add(newRole);
                                //goldContext.Add(role);

                                foreach (var nation in person.PersonCountries)
                                {
                                    countriesToSave.Add(
                                        new PersonCountry()
                                        {
                                            Person = newPerson,
                                            CountryId = countryMap[nation.CountryId.Value]
                                        }
                                        );
                                }

                            }
                            companiesToSave.Add(newCompany);
                        }

                        goldContext.SaveChanges();

                        foreach (var c in companiesToSave)
                        {
                            goldContext.Add(c);
                        }

                        foreach (var cn in namesToSave)
                        {
                            goldContext.Add(cn);
                        }

                        foreach (var r in rolesToSave)
                        {
                            goldContext.Add(r);
                        }

                        foreach (var p in personsToSave)
                        {
                            goldContext.Add(p);
                        }

                        goldContext.SaveChanges();

                        foreach (var c in countriesToSave)
                        {
                            goldContext.Add(c);
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
            foreach (var industry in silverContext.Industries.AsQueryable()
                    .Include(x => x.IndustryCountries))
            {
                var newIndustry = new Industry()
                {
                    Name = industry.Name,
                };

                if (industry.IndustryCountries.Count > 0)
                {
                    var industryCountry = industry.IndustryCountries.First();
                    newIndustry.IndustryCountries.Add(
                        new IndustryCountry()
                        {
                            Industry = newIndustry,
                            AvgPay = industryCountry.AvgPay,
                            DiPledge = industryCountry.DiPledge,
                            DisabilitiesPledge = industryCountry.DisabilitiesPledge,
                            EducationSpend = industryCountry.EducationSpend,
                            FlexibleHoursPledge = industryCountry.FlexibleHoursPledge,
                            HarassmentPledge = industryCountry.HarassmentPledge,
                            IndustryDiversity = industryCountry.IndustryDiversity,
                            LgbtPledge = industryCountry.LgbtPledge,
                            MaterintyLeavePledge = industryCountry.MaterintyLeavePledge,
                            NumEmployees = industryCountry.NumEmployees,
                            PaternityLeavePledge = industryCountry.PaternityLeavePledge,
                            RententionRate = industryCountry.RententionRate,
                            WomenEmployeedPercent = industryCountry.WomenEmployeedPercent,
                        });
                    goldContext.Add(newIndustry.IndustryCountries.First());
                }

                goldContext.Add(newIndustry);
            }

            foreach (var industryCountry in silverContext.IndustryCountries.AsQueryable()
                    .Where(x => x.Industry == null))
            {
                goldContext.Add(new IndustryCountry()
                {
                    AvgPay = industryCountry.AvgPay,
                    DiPledge = industryCountry.DiPledge,
                    DisabilitiesPledge = industryCountry.DisabilitiesPledge,
                    EducationSpend = industryCountry.EducationSpend,
                    FlexibleHoursPledge = industryCountry.FlexibleHoursPledge,
                    HarassmentPledge = industryCountry.HarassmentPledge,
                    IndustryDiversity = industryCountry.IndustryDiversity,
                    LgbtPledge = industryCountry.LgbtPledge,
                    MaterintyLeavePledge = industryCountry.MaterintyLeavePledge,
                    NumEmployees = industryCountry.NumEmployees,
                    PaternityLeavePledge = industryCountry.PaternityLeavePledge,
                    RententionRate = industryCountry.RententionRate,
                    WomenEmployeedPercent = industryCountry.WomenEmployeedPercent,
                });
            }

            goldContext.SaveChanges();

            Logger.Log("Industries loaded");
        }

        private void LoadCountries(SilverContext silverContext, GoldContext goldContext)
        {
            Logger.Log("Loading countries");
            foreach (var country in silverContext.Countries.AsQueryable()
                    .Include(x => x.CountryAges)
                    .Include(x => x.CountryDemographics)
                    .Include(x => x.CountryDisabilities)
                    .Include(x => x.CountryEconomies)
                    .Include(x => x.CountryEdus)
                    .Include(x => x.CountryGenders)
                    .Include(x => x.CountryPoliticals)
                    .Include(x => x.CountryRaces)
                    .Include(x => x.CountryReligions)
                    .Include(x => x.CountrySexes)
                    .Include(x => x.CountryUrbans))
            {
                var newCountry = new Country()
                {
                    Code = country.Code,
                    Name = country.Name,
                };

                if (country.CountryAges.Count > 0)
                {
                    var countryAge = country.CountryAges.First();
                    newCountry.CountryAges.Add(
                        new DB.Gold.Entities.CountryData.CountryAge()
                        {
                            Country = newCountry,
                            Avg18 = countryAge.Avg18,
                            Dis19 = countryAge.Dis19,
                            Dis39 = countryAge.Dis39,
                            Dis59 = countryAge.Dis59,
                            Dis70 = countryAge.Dis70,
                            Disx = countryAge.Disx,
                            Ministers = countryAge.Ministers,
                            Parliament = countryAge.Parliament,
                        });
                    goldContext.Add(newCountry.CountryAges.First());
                }

                if (country.CountryDemographics.Count > 0)
                {
                    var countryDemographics = country.CountryDemographics.First();
                    newCountry.CountryDemographics.Add(
                        new DB.Gold.Entities.CountryData.CountryDemographic()
                        {
                            Country = newCountry,
                            ImmigrantPercent = countryDemographics.ImmigrantPercent,
                            ImmigrantPop = countryDemographics.ImmigrantPop,
                            Population = countryDemographics.Population,
                        });
                    goldContext.Add(newCountry.CountryDemographics.First());
                }

                if (country.CountryDisabilities.Count > 0)
                {
                    var countryDisability = country.CountryDisabilities.First();
                    newCountry.CountryDisabilities.Add(
                        new DB.Gold.Entities.CountryData.CountryDisability()
                        {
                            Country = newCountry,
                            Disabled = countryDisability.Disabled,
                            DiscriminationLaw = countryDisability.DiscriminationLaw,
                            HealthFundingGdp = countryDisability.HealthFundingGdp,
                            HealthFundingType = countryDisability.HealthFundingType,
                            Overweight = countryDisability.Overweight,
                        });
                    goldContext.Add(newCountry.CountryDisabilities.First());
                }

                if (country.CountryEconomies.Count > 0)
                {
                    var countryEconomy = country.CountryEconomies.First();
                    newCountry.CountryEconomies.Add(
                        new DB.Gold.Entities.CountryData.CountryEconomy()
                        {
                            Country = newCountry,
                            AvgIncome = countryEconomy.AvgIncome,
                            EqualityLevel = countryEconomy.EqualityLevel,
                            FemaleUnemploy = countryEconomy.FemaleUnemploy,
                            Gdp = countryEconomy.Gdp,
                            GdpPerCapita = countryEconomy.GdpPerCapita,
                            GdpWorld = countryEconomy.GdpWorld,
                            LabourForce = countryEconomy.LabourForce,
                            LabourForcePercent = countryEconomy.LabourForcePercent,
                            MaleUnemploy = countryEconomy.MaleUnemploy,
                            Poor = countryEconomy.Poor,
                        });
                    goldContext.Add(newCountry.CountryEconomies.First());
                }

                if (country.CountryEdus.Count > 0)
                {
                    var countryEdu = country.CountryEdus.First();
                    newCountry.CountryEdus.Add(
                        new DB.Gold.Entities.CountryData.CountryEdu()
                        {
                            Country = newCountry,
                            ActualEducation = countryEdu.ActualEducation,
                            BachelorFemale = countryEdu.BachelorFemale,
                            BachelorMale = countryEdu.BachelorMale,
                            BachelorMf = countryEdu.BachelorMf,
                            ElementaryFemale = countryEdu.ElementaryFemale,
                            ElementaryMale = countryEdu.ElementaryMale,
                            ElementaryMf = countryEdu.ElementaryMf,
                            ExpectedEducation = countryEdu.ExpectedEducation,
                            HighSchoolFemale = countryEdu.HighSchoolFemale,
                            HighSchoolMale = countryEdu.HighSchoolMale,
                            HighSchoolMf = countryEdu.HighSchoolMf,
                            MasterFemale = countryEdu.MasterFemale,
                            MasterMale = countryEdu.MasterMale,
                            MasterMf = countryEdu.MasterMf,
                            PublicFundFund = countryEdu.PublicFundFund,
                            PublicFundingGdp = countryEdu.PublicFundingGdp,
                            TotalMf = countryEdu.TotalMf,
                        });
                    goldContext.Add(newCountry.CountryEdus.First());
                }

                if (country.CountryGenders.Count > 0)
                {
                    var countryGender = country.CountryGenders.First();
                    newCountry.CountryGenders.Add(
                        new DB.Gold.Entities.CountryData.CountryGender()
                        {
                            Country = newCountry,
                            FemaleMinisterShare = countryGender.FemaleMinisterShare,
                            FemaleParliamentShare = countryGender.FemaleParliamentShare,
                            FemalePop = countryGender.FemalePop,
                            FemalePromotionPolicy = countryGender.FemalePromotionPolicy,
                            FemaleWorkForce = countryGender.FemaleWorkForce,
                            FemaleWorkForcePercent = countryGender.FemaleWorkForcePercent,
                            FemaleWorkForcePercentPop = countryGender.FemaleWorkForcePercentPop,
                            GenderEduGap = countryGender.GenderEduGap,
                            GenderHealthGap = countryGender.GenderHealthGap,
                            GenderPolGap = countryGender.GenderPolGap,
                            GenderWorkGap = countryGender.GenderWorkGap,
                            IncomeGap = countryGender.IncomeGap,
                            LifeFemale = countryGender.LifeFemale,
                            LifeMale = countryGender.LifeMale,
                            MalePop = countryGender.MalePop,
                            MaterintyLeave = countryGender.MaterintyLeave,
                            PaternityLeave = countryGender.PaternityLeave,
                            WomenEdu = countryGender.WomenEdu,
                            WomenViolence = countryGender.WomenViolence,
                        });
                    goldContext.Add(newCountry.CountryGenders.First());
                }

                if (country.CountryPoliticals.Count > 0)
                {
                    var countryPolitical = country.CountryPoliticals.First();
                    newCountry.CountryPoliticals.Add(
                        new DB.Gold.Entities.CountryData.CountryPolitical()
                        {
                            Country = newCountry,
                            Corruption = countryPolitical.Corruption,
                            Democracy = countryPolitical.Democracy,
                            FreedomSpeech = countryPolitical.FreedomSpeech,

                        });
                    goldContext.Add(newCountry.CountryPoliticals.First());
                }

                if (country.CountryRaces.Count > 0)
                {
                    var countryRace = country.CountryRaces.First();
                    newCountry.CountryRaces.Add(
                        new DB.Gold.Entities.CountryData.CountryRace()
                        {
                            Country = newCountry,
                            Arab = countryRace.Arab,
                            Asian = countryRace.Asian,
                            Black = countryRace.Black,
                            Caucasian = countryRace.Caucasian,
                            DiscriminationLaw = countryRace.DiscriminationLaw,
                            Hispanic = countryRace.Hispanic,
                            Indegineous = countryRace.Indegineous,
                        });
                    goldContext.Add(newCountry.CountryRaces.First());
                }

                if (country.CountryReligions.Count > 0)
                {
                    var countryReligion = country.CountryReligions.First();
                    newCountry.CountryReligions.Add(
                        new DB.Gold.Entities.CountryData.CountryReligion()
                        {
                            Country = newCountry,
                            Buddishm = countryReligion.Buddishm,
                            Christian = countryReligion.Christian,
                            Freedom = countryReligion.Freedom,
                            Hindu = countryReligion.Hindu,
                            Judaism = countryReligion.Judaism,
                            Muslim = countryReligion.Muslim,
                            Statereligion = countryReligion.Statereligion,
                        });
                    goldContext.Add(newCountry.CountryReligions.First());
                }

                if (country.CountrySexes.Count > 0)
                {
                    var countrySex = country.CountrySexes.First();
                    newCountry.CountrySexes.Add(
                        new DB.Gold.Entities.CountryData.CountrySex()
                        {
                            Country = newCountry,
                            HomosexualPop = countrySex.HomosexualPop,
                            HomosexualTolerance = countrySex.HomosexualTolerance,
                            SameAdopt = countrySex.SameAdopt,
                            SameMarriage = countrySex.SameMarriage,
                        });
                    goldContext.Add(newCountry.CountrySexes.First());
                }

                if (country.CountryUrbans.Count > 0)
                {
                    var countryUrban = country.CountryUrbans.First();
                    newCountry.CountryUrbans.Add(
                        new DB.Gold.Entities.CountryData.CountryUrban()
                        {
                            Country = newCountry,
                            CitiesPop = countryUrban.CitiesPop,
                        });
                    goldContext.Add(newCountry.CountryUrbans.First());
                }

                goldContext.Add(newCountry);
            }

            goldContext.SaveChanges();

            var oldCountries = silverContext.Countries.Select(x => new { x.Name, x.Id }).ToDictionary(x => x.Id, x => x.Name);

            var newCountries = goldContext.Country.Select(x => new { x.Name, x.Id }).ToDictionary(x => x.Name, x => x.Id);

            foreach (var id in oldCountries.Keys)
            {
                countryMap[id] = newCountries[oldCountries[id]];
            }

            Logger.Log("Countries loaded");
        }
    }
}
