using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Amazon.Lambda.Core;
using DigitalInsights.Common.Logging;
using DigitalInsights.DB.Lead;
using DigitalInsights.DB.Silver;
using DigitalInsights.DB.Silver.Entities;
using System.Threading;
using Microsoft.Extensions.Logging;
using DigitalInsights.DB.Lead.GLEIF;
using System.Diagnostics;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DigitalInsights.DataLoaders.Silver.GLEIFLoader
{
    public class GLEIFLoader
    {
        const string TRANSFORMER_NAME = "GLEIF LOADER";

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

                Logger.Log($"Creating DB contexts.");
                var silverContext = new SilverContext();
                var leadContext = new LeadContext();

                Dictionary<string, Country> countries = silverContext.Countries.ToDictionary(x => x.Code, x => x);

                if (countries.Count == 0)
                {
                    throw new InvalidOperationException("Countries are not loaded");
                }


                List<string> leis;

                leis = leadContext.GleifEntity.Select(x => x.Lei)
                    //.Take(1000)
                    .ToList();

                //using (StreamWriter sw = new StreamWriter("C:\\temp\\leis.dat"))
                //{
                //    foreach (var s in leis)
                //    {
                //        sw.WriteLine(s);
                //    }
                //}

                //leis = new List<string>(1700000);

                //using (StreamReader sr = new StreamReader("C:\\temp\\leis.dat"))
                //{
                //    while(!sr.EndOfStream)
                //    {
                //        leis.Add(sr.ReadLine());
                //    }
                //}

                var chunks = new List<List<string>>();

                int size = 10000;

                for (int i = 0; i < leis.Count; i += size)
                {
                    chunks.Add(leis.GetRange(i, Math.Min(size, leis.Count - i)));
                }

                chunks
                    .AsParallel().WithDegreeOfParallelism(24).ForAll(
                    //.Skip(20).ToList()
                    //.ForEach(
                    x => LoadChunk(x, chunks.IndexOf(x), countries));

                sw.Stop();
                Logger.Log($"Finished successfully in {sw.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Logger.Log($"ERROR: Unhandled exception: {ex.ToString()}");
            }
        }

        public void LoadChunk(List<string> chunk, int index, Dictionary<string, Country> countries)
        {
            try
            {
                Logger.Log($"Loading chunk {index}");
                var leadContext = new LeadContext();
                //var factory = LoggerFactory.Create(builder =>
                //{
                //    builder.AddProvider(new TraceLoggerProvider());
                //});
                var silverContext = new SilverContext();
                silverContext.ChangeTracker.LazyLoadingEnabled = true;
                silverContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                silverContext.ChangeTracker.AutoDetectChangesEnabled = false;

                List<Company> companiesToSave = new List<Company>();
                List<CompanyName> namesToSave = new List<CompanyName>();
                foreach (var entity in leadContext.GleifEntity.AsNoTracking().Where(x => chunk.Contains(x.Lei)).Include(x => x.GleifAddresses).Include(x => x.GleifEntityNames))
                {
                    //var targetEntity = silverContext.Company.Where(x => x.Lei == entity.Lei).FirstOrDefault();
                    //if (targetEntity == null)
                    //{

                    if (!countries.ContainsKey(entity.LegalJurisdiction.Split('-')[0]))
                    {
                        // we have no country data for it
                        continue;
                    }

                    var targetEntity = new Company();
                    companiesToSave.Add(targetEntity);
                    //}

                    var names = entity.GleifEntityNames;
                    var legalName = names.Where(x => x.Type == "LEGAL").First(); //legal name should exist
                    var otherNames = names.Where(x => x.Type != "LEGAL" && x.Type != "PREVIOUS_LEGAL_NAME" && !string.IsNullOrEmpty(x.Name)).ToList();
                    targetEntity.LegalName = legalName.Name;

                    foreach (var name in otherNames)
                    {
                        var otherName = new CompanyName()
                        {
                            Company = targetEntity,
                            Name = name.Name,
                            Type = name.Type,
                        };
                        targetEntity.CompanyNames.Add(otherName);
                        namesToSave.Add(otherName);
                    }

                    targetEntity.Status = entity.Status;
                    targetEntity.Lei = entity.Lei;

                    var legalAddress = entity.GleifAddresses.Where(x => x.Type == "LEGAL").First();

                    if (targetEntity.Legal == null)
                    {
                        targetEntity.Legal = new Address();
                        silverContext.Add(targetEntity.Legal);
                        //targetEntity.Legal.CompanyLegal.Add(targetEntity);
                    }

                    Address targetLegalAddress = targetEntity.Legal;
                    targetLegalAddress.City = legalAddress.City;
                    if (countries.ContainsKey(legalAddress.Country))
                    {
                        targetLegalAddress.Country = countries[legalAddress.Country];
                        targetLegalAddress.CountryId = targetLegalAddress.Country.Id;
                    }
                    targetLegalAddress.AddressNumber = legalAddress.Addressnumber;
                    targetLegalAddress.PostalCode = legalAddress.Postalcode;
                    targetLegalAddress.Region = legalAddress.Region;
                    targetLegalAddress.AddressLine = legalAddress.Firstaddressline
                        + (string.IsNullOrEmpty(legalAddress.Additionaladdressline1) ? string.Empty : " " + legalAddress.Additionaladdressline1)
                        + (string.IsNullOrEmpty(legalAddress.Additionaladdressline2) ? string.Empty : " " + legalAddress.Additionaladdressline2)
                        + (string.IsNullOrEmpty(legalAddress.Additionaladdressline3) ? string.Empty : " " + legalAddress.Additionaladdressline3);
                    targetLegalAddress.AddressLine = targetLegalAddress.AddressLine ?? string.Empty;

                    var hqAddress = entity.GleifAddresses.Where(x => x.Type == "HQ").First();

                    if (targetEntity.Hq == null)
                    {
                        targetEntity.Hq = new Address();
                        silverContext.Add(targetEntity.Hq);
                        //targetEntity.Hq.CompanyHq.Add(targetEntity);
                    }

                    Address targetHqAddress = targetEntity.Hq;
                    targetHqAddress.City = hqAddress.City;
                    if (countries.ContainsKey(hqAddress.Country))
                    {
                        targetHqAddress.Country = countries[hqAddress.Country];
                        targetHqAddress.CountryId = targetHqAddress.Country.Id;
                    }
                    targetHqAddress.AddressNumber = hqAddress.Addressnumber;
                    targetHqAddress.PostalCode = hqAddress.Postalcode;
                    targetHqAddress.Region = hqAddress.Region;
                    targetHqAddress.AddressLine = hqAddress.Firstaddressline
                        + (string.IsNullOrEmpty(hqAddress.Additionaladdressline1) ? string.Empty : " " + hqAddress.Additionaladdressline1)
                        + (string.IsNullOrEmpty(hqAddress.Additionaladdressline2) ? string.Empty : " " + hqAddress.Additionaladdressline2)
                        + (string.IsNullOrEmpty(hqAddress.Additionaladdressline3) ? string.Empty : " " + hqAddress.Additionaladdressline3);
                    targetHqAddress.AddressLine = targetHqAddress.AddressLine ?? string.Empty;

                    targetEntity.CompanyCountries.Add(
                        new CompanyCountry()
                        {
                            IsPrimary = true,
                            LegalJurisdiction = true,
                            Company = targetEntity,
                            CountryId = countries[entity.LegalJurisdiction.Split('-')[0]].Id
                        });
                }

                silverContext.SaveChanges();

                foreach (var company in companiesToSave)
                {
                    silverContext.Add(company);
                }

                foreach (var name in namesToSave)
                {
                    silverContext.Add(name);
                }
                silverContext.SaveChanges();
                silverContext.Dispose();
                leadContext.Dispose();
                Logger.Log($"Chunk {index} loaded");
            }
            catch (Exception ex)
            {
                Logger.Log($"ERROR: Unhandled exception: {ex.ToString()}");
                throw;
            }
        }
    }
}
