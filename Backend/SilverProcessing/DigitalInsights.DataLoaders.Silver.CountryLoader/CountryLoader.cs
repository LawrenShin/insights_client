using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using CsvHelper;
using DigitalInsights.Common.Logging;
using DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV;
using DigitalInsights.DB.Silver;
using DigitalInsights.DB.Silver.Entities;
using DigitalInsights.DB.Silver.Entities.CountryData;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DigitalInsights.DataLoaders.Silver.CountryLoader
{
    public class CountryLoader
    {
        const string TRANSFORMER_NAME = "COUNTRY LOADER";

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public void FunctionHandler(ILambdaContext context)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Logger.Init(TRANSFORMER_NAME);
                Logger.Log("Started");
                // todo: switch to S3
                var filename = "C:\\temp\\country.csv";
                File.WriteAllText(filename, File.ReadAllText(filename).Replace(',', '.'));
                var str = File.ReadAllText(filename);
                str = str.Replace(',', '.');
                File.WriteAllText(filename, str);

                Logger.Log("Configuring DB context.");
                SilverContext dbContext = new SilverContext();
                dbContext.ChangeTracker.LazyLoadingEnabled = true;

                var countries = dbContext.Countries.ToList();

                using (var fileReader = new StreamReader(filename))
                {
                    Logger.Log("Configuring CSV reader");
                    var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                    csvReader.Configuration.Delimiter = ";";
                    csvReader.Configuration.MissingFieldFound = null;
                    csvReader.Configuration.RegisterClassMap<CountryAgeMap>();
                    csvReader.Configuration.RegisterClassMap<CountryDemographicsMap>();
                    csvReader.Configuration.RegisterClassMap<CountryDisabilityMap>();
                    csvReader.Configuration.RegisterClassMap<CountryEconomicPowerMap>();
                    csvReader.Configuration.RegisterClassMap<CountryEconomicEqualityMap>();
                    csvReader.Configuration.RegisterClassMap<CountryEducationMap>();
                    csvReader.Configuration.RegisterClassMap<CountryGenderMap>();
                    csvReader.Configuration.RegisterClassMap<CountryLaborForceMap>();
                    csvReader.Configuration.RegisterClassMap<CountryMap>();
                    csvReader.Configuration.RegisterClassMap<CountryPoliticalMap>();
                    csvReader.Configuration.RegisterClassMap<CountryRaceMap>();
                    csvReader.Configuration.RegisterClassMap<CountryReligionMap>();
                    csvReader.Configuration.RegisterClassMap<CountrySexualityMap>();
                    csvReader.Configuration.RegisterClassMap<CountryUrbanizationMap>();

                    while (csvReader.Read())
                    {
                        var country = csvReader.GetRecord<Country>();

                        var targetCountry = countries.First(x => x.Name == country.Name.Trim());

                        var countryAge = csvReader.GetRecord<CountryAge>();
                        countryAge.Country = targetCountry;
                        targetCountry.CountryAges.Add(countryAge);
                        dbContext.Add(countryAge);

                        var countryDemographic = csvReader.GetRecord<CountryDemographics>();
                        countryDemographic.Country = targetCountry;
                        targetCountry.CountryDemographics.Add(countryDemographic);
                        dbContext.Add(countryDemographic);

                        var countryDisability = csvReader.GetRecord<CountryDisability>();
                        countryDisability.Country = targetCountry;
                        targetCountry.CountryDisabilities.Add(countryDisability);
                        dbContext.Add(countryDisability);

                        var countryEconomy = csvReader.GetRecord<CountryEconomicPower>();
                        countryEconomy.Country = targetCountry;
                        targetCountry.CountryEconomicPowers.Add(countryEconomy);
                        dbContext.Add(countryEconomy);

                        var countryEconomicEquality = csvReader.GetRecord<CountryEconomicEquality>();
                        countryEconomy.Country = targetCountry;
                        targetCountry.CountryEconomicEqualities.Add(countryEconomicEquality);
                        dbContext.Add(countryEconomicEquality);

                        var countryEdu = csvReader.GetRecord<CountryEducation>();
                        countryEdu.Country = targetCountry;
                        targetCountry.CountryEducations.Add(countryEdu);
                        dbContext.Add(countryEdu);

                        var countryGender = csvReader.GetRecord<CountryGender>();
                        countryGender.Country = targetCountry;
                        targetCountry.CountryGenders.Add(countryGender);
                        dbContext.Add(countryGender);

                        var countryLaborForce = csvReader.GetRecord<CountryLaborForce>();
                        countryGender.Country = targetCountry;
                        targetCountry.CountryLaborForces.Add(countryLaborForce);
                        dbContext.Add(countryLaborForce);

                        var countryPolitical = csvReader.GetRecord<CountryPolitical>();
                        countryPolitical.Country = targetCountry;
                        targetCountry.CountryPoliticals.Add(countryPolitical);
                        dbContext.Add(countryPolitical);

                        var countryRace = csvReader.GetRecord<CountryRace>();
                        countryRace.Country = targetCountry;
                        targetCountry.CountryRaces.Add(countryRace);
                        dbContext.Add(countryRace);

                        var countryReligion = csvReader.GetRecord<CountryReligion>();
                        countryReligion.Country = targetCountry;
                        targetCountry.CountryReligions.Add(countryReligion);
                        dbContext.Add(countryReligion);

                        var countrySex = csvReader.GetRecord<CountrySexuality>();
                        countrySex.Country = targetCountry;
                        targetCountry.CountrySexualities.Add(countrySex);
                        dbContext.Add(countrySex);

                        var countryUrban = csvReader.GetRecord<CountryUrbanization>();
                        countryUrban.Country = targetCountry;
                        targetCountry.CountryUrbanizations.Add(countryUrban);
                        dbContext.Add(countryUrban);

                        // empty values for currently missing values
                        var countryLASP = new CountryLaborAndSocialProtection();
                        countryLASP.Country = targetCountry;
                        targetCountry.CountryLaborAndSocialProtections.Add(countryLASP);
                        dbContext.Add(countryLASP);

                        var countryInfrastructure = new CountryInfrastructure();
                        countryInfrastructure.Country = targetCountry;
                        targetCountry.CountryInfrastructures.Add(countryInfrastructure);
                        dbContext.Add(countryInfrastructure);

                        var countryPSAT = new CountryPrivateSectorAndTrade();
                        countryPSAT.Country = targetCountry;
                        targetCountry.CountryPrivateSectorsAndTrades.Add(countryPSAT);
                        dbContext.Add(countryPSAT);

                        var countryPublicSector = new CountryPublicSector();
                        countryPublicSector.Country = targetCountry;
                        targetCountry.CountryPublicSectors.Add(countryPublicSector);
                        dbContext.Add(countryPublicSector);

                        var countryUtility = new CountryUtility();
                        countryUtility.Country = targetCountry;
                        targetCountry.CountryUtilities.Add(countryUtility);
                        dbContext.Add(countryUtility);
                    }
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
