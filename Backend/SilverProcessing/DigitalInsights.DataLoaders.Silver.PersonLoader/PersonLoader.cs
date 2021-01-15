using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Amazon.Lambda.Core;
using CsvHelper;
using DigitalInsights.Common.Logging;
using DigitalInsights.DB.Silver;
using DigitalInsights.DB.Silver.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DigitalInsights.DataLoaders.Silver.PersonLoader.Model.CSV;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DigitalInsights.DataLoaders.Silver.PersonLoader
{
    public class PersonLoader
    {

        const string TRANSFORMER_NAME = "PersonLoader";

        /// <summary>
        /// Loader of CEOs and board members
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public void FunctionHandler(ILambdaContext context)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Logger.Init(TRANSFORMER_NAME, "{2}");
                Logger.Log("Started");
                // todo: switch to S3
                var filename = "C:\\temp\\Companies.csv";

                Logger.Log("Configuring DB context.");

                var factory = LoggerFactory.Create(builder =>
                {
                    builder.AddProvider(new TraceLoggerProvider());
                });
                var dbContext = new SilverContext(factory);

                dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                dbContext.ChangeTracker.LazyLoadingEnabled = true;

                var countries = dbContext.Countries.Select(x => new { x.Name, x.Id }).ToDictionary(x => x.Name, x => x.Id);
                countries["UAE"] = countries["United Arab Emirates"];
                countries["Russia"] = countries["Russian Federation"];
                countries["England"] = countries["United Kingdom"];
                countries["Great Britain"] = countries["United Kingdom"];
                countries["Palestine"] = countries["State of Palestine"];
                foreach (var key in countries.Keys)
                {
                    Logger.Log(key);
                }
                var matchesRaw = dbContext.CompanyMatches.AsNoTracking()
                    .Where(x => x.CompanyId != null)
                    .ToList();

                var matches = matchesRaw
                    .GroupBy(x => x.Name)
                    .Where(x => x.Count() == 1)
                    .SelectMany(x => x)
                    .ToDictionary(x => x.Name, x => x.CompanyId);

                List<Tuple<Role, Person>> rolesToAdd = new List<Tuple<Role, Person>>();

                HashSet<string> companies = new HashSet<string>();

                // preload relevant countries
                using (var fileReader = new StreamReader(filename))
                {
                    Logger.Log("Configuring CSV reader");
                    var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                    csvReader.Configuration.Delimiter = ";";
                    csvReader.Configuration.MissingFieldFound = null;
                    csvReader.Configuration.RegisterClassMap<PersonMap>();
                    csvReader.Configuration.RegisterClassMap<RoleMap>();
                    bool doOnce = false;
                    while (csvReader.Read())
                    {
                        if (!doOnce)
                        {
                            var role = csvReader.GetRecord<Role>();
                            doOnce = true;
                        }
                        companies.Add(csvReader.GetField(0));
                    }
                }

                var companyIds = companies.Where(x => matches.ContainsKey(x)).Select(x => matches[x]).Where(x => x != -1).ToList();
                Dictionary<int, Company> companiesLookup =
                    dbContext.Companies.AsQueryable().Include(x => x.Roles).Where(x => companyIds.Contains(x.Id))
                    .ToDictionary(x => x.Id, x => x);

                using (var fileReader = new StreamReader(filename))
                {
                    Logger.Log("Configuring CSV reader");
                    var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                    csvReader.Configuration.Delimiter = ";";
                    csvReader.Configuration.MissingFieldFound = null;
                    csvReader.Configuration.RegisterClassMap<PersonMap>();
                    csvReader.Configuration.RegisterClassMap<RoleMap>();

                    string prevCompanyName = "";
                    Company targetCompany = null;
                    Person previousPerson = null;

                    while (csvReader.Read())
                    {
                        var role = csvReader.GetRecord<Role>();
                        var person = csvReader.GetRecord<Person>();
                        //if (person.Urban == "0") person.Urban = null;


                        var companyName = csvReader.GetField(0); // company name

                        if (companyName != prevCompanyName)
                        {
                            if (matches.ContainsKey(companyName))
                            {
                                var companyId = matches[companyName];

                                prevCompanyName = companyName;

                                if (companyId == -1 || !companiesLookup.ContainsKey(companyId.Value))
                                {
                                    targetCompany = null;
                                    continue;
                                }

                                targetCompany = companiesLookup[companyId.Value];
                                if (targetCompany == null)
                                {
                                    throw new InvalidOperationException($"Company is not found for ID {companyId}");
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }

                        if (targetCompany == null) continue;

                        if (previousPerson == null || person.Name != previousPerson.Name)
                        {
                            previousPerson = person;
                            dbContext.Add(person);

                            Role targetRole;
                            if ((targetRole = targetCompany.Roles.FirstOrDefault(x => x.RoleType == role.RoleType && x.Title == role.Title)) == null)
                            {
                                role.Company = targetCompany;
                                rolesToAdd.Add(Tuple.Create(role, person));
                            }

                            var primaryNation = csvReader.GetField("Primary Nation").Trim();
                            primaryNation = primaryNation.Substring(0, 1).ToUpperInvariant() + primaryNation.Substring(1);
                            if (!string.IsNullOrEmpty(primaryNation) && primaryNation != "0")
                            {
                                if (countries.ContainsKey(primaryNation))
                                {
                                    var pc = new PersonCountry()
                                    {
                                        CountryId = countries[primaryNation],
                                        Person = person
                                    };

                                    person.PersonCountries.Add(pc);
                                    dbContext.Add(pc);
                                }
                                else
                                {
                                    Logger.Log($"No country found - {primaryNation}");
                                }
                            }

                            var secondaryNation = csvReader.GetField("Secondary Nation").Trim();
                            secondaryNation = secondaryNation.Substring(0, 1).ToUpperInvariant() + secondaryNation.Substring(1);
                            if (!string.IsNullOrEmpty(secondaryNation) && secondaryNation != "0")
                            {
                                if (countries.ContainsKey(secondaryNation))
                                {
                                    var pc = new PersonCountry()
                                    {
                                        CountryId = countries[secondaryNation],
                                        Person = person
                                    };

                                    person.PersonCountries.Add(pc);

                                    dbContext.Add(pc);
                                }
                                else
                                {
                                    Logger.Log($"No country found - {secondaryNation}");
                                }
                            }
                        }
                    }
                }

                dbContext.SaveChanges();

                foreach (var tuple in rolesToAdd)
                {
                    tuple.Item1.PersonId = tuple.Item2.Id;
                    dbContext.Add(tuple.Item1);
                }

                dbContext.SaveChanges();

                dbContext.Dispose();

                sw.Stop();
                Logger.Log($"Finished successfully in {sw.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Logger.Log($"ERROR: Unhandled exception: {ex.ToString()}");
            }
        }
    }
}
