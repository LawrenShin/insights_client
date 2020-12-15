using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using Amazon;
using Amazon.Lambda.Core;
using Amazon.S3;
using CsvHelper;
using DigitalInsights.Common.Logging;
using DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.API;
using DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.CSV;
using DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.CSV.Auxiliary;
using DigitalInsights.DB.Lead;
using DigitalInsights.DB.Lead.GLEIF;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DigitalInsights.DataLoaders.Lead.GLEIFLoader
{
    public class GLEIFLoader
    {
        const string CRAWLER_NAME = "GLEIF Loader";

        const int DB_FLUSH_THRESHOLD = 1000;

        const string GLEIF_SERVICE = "https://leidata-preview.gleif.org/api/v2/golden-copies/publishes?page=1&per_page=1";

        private int THREAD_COUNT = Environment.ProcessorCount;

        Semaphore total;

        public GLEIFLoader()
        {
            total = new Semaphore(0, THREAD_COUNT);
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public void FunctionHandler(ILambdaContext context)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Logger.Init(CRAWLER_NAME);
                Logger.Log("Started");

                string fileUrl = string.Empty;

                Logger.Log("Asking GLEIF service for file URL");
                WebRequest fileAddressRequest = WebRequest.Create(GLEIF_SERVICE);
                using (var stream = fileAddressRequest.GetResponse().GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        string fileAddressStringResponse = sr.ReadToEnd();
                        Logger.Log("Recieved GLEIF service response for file URL");
                        var fileAddressResponse = JsonConvert.DeserializeObject<ServiceResponse>(fileAddressStringResponse);
                        Logger.Log("Successfully parsed GLEIF service response for file URL");
                        fileUrl = fileAddressResponse.Data.First().Lei2.FullFile.Csv.Url;
                    }
                }

                if (string.IsNullOrEmpty(fileUrl))
                {
                    throw new InvalidOperationException("GLEIF file URL was not obtained properly!");
                }
                else
                {
                    Logger.Log($"GLEIF file URL is {fileUrl}");
                }

                //Log("Downloading GLEIF file to S3");

                string s3FileKey = $"GLEIF/{DateTime.Now.ToString("yyyyMMdd")}.csv";
                var s3Client = new AmazonS3Client(RegionEndpoint.USEast2);


                Logger.Log("Starting reading Golden Copy file contents");
                WebRequest fileRequest = WebRequest.Create(fileUrl);
                var fileResponse = fileRequest.GetResponse();

                using (Stream pageStream = fileResponse.GetResponseStream())
                //using (Stream pageStream = new FileStream("C:\\temp\\20201102.zip", FileMode.Open, FileAccess.Read))
                {
                    var archive = new ZipArchive(pageStream, ZipArchiveMode.Read);
                    var entry = archive.Entries.First();
                    using (var zippedFileStream = entry.Open())
                    {
                        using (var fileReader = new StreamReader(zippedFileStream))
                        {

                            CsvReader csvReader = ConfigureCsvReader(fileReader);

                            Logger.Log("Reading infrastructure for Golden Copy file contents initialized.");

                            Logger.Log("Configuring DB context.");

                            //using (var sw = new StreamWriter("C:\\temp\\allnames.dat"))
                            //{

                            //    while (csvReader.Read())
                            //    {
                            //        sw.WriteLine(csvReader.GetRecord<LegalEntityName>().Name.ToUpperInvariant());
                            //    }
                            //}

                            LeadContext dbContext = new LeadContext();
                            dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                            dbContext.ChangeTracker.LazyLoadingEnabled = true;
                            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                            Logger.Log("Clearing Lead GLEIF tables.");
                            //dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE gleif_validation_authority");
                            dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE gleif_address");
                            dbContext.Database.ExecuteSqlRaw("DELETE FROM gleif_entity_name");
                            dbContext.Database.ExecuteSqlRaw("DELETE FROM gleif_entity");
                            dbContext.Dispose();
                            Logger.Log("Lead GLEIF tables cleared.");

                            for (int i = 0; i < THREAD_COUNT; i++)
                            {
                                new Thread(x => UploadChunks(csvReader)).Start();
                            }
                            for (int i = 0; i < THREAD_COUNT; i++)
                            {
                                total.WaitOne();
                            }
                            Logger.Log($"Processing finished.");
                        }
                    }
                }
                sw.Stop();
                Logger.Log($"Finished successfully in {sw.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Logger.Log($"ERROR: Unhandled exception: {ex.ToString()}");
            }
        }

        private void UploadChunks(CsvReader csvReader)
        {
            while (true)
            {
                LeadContext localDbContext = new LeadContext();
                localDbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                localDbContext.ChangeTracker.LazyLoadingEnabled = true;
                localDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                bool canRead = true;
                lock (this)
                {
                    int counter = 0;
                    while ((canRead = csvReader.Read()) && counter < DB_FLUSH_THRESHOLD)
                    {

                        var entity = csvReader.GetRecord<GleifEntity>();

                        new List<GleifEntityName>()
                                    {
                                        csvReader.GetRecord<LegalEntityName>(),
                                        csvReader.GetRecord<OtherEntityName1>(),
                                        csvReader.GetRecord<OtherEntityName2>(),
                                        csvReader.GetRecord<OtherEntityName3>(),
                                        csvReader.GetRecord<OtherEntityName4>(),
                                        csvReader.GetRecord<OtherEntityName5>(),
                                        csvReader.GetRecord<TransliteratedOtherEntityName1>(),
                                        csvReader.GetRecord<TransliteratedOtherEntityName2>(),
                                        csvReader.GetRecord<TransliteratedOtherEntityName3>(),
                                        csvReader.GetRecord<TransliteratedOtherEntityName4>(),
                                        csvReader.GetRecord<TransliteratedOtherEntityName5>()
                                    }
                        .Where(x => !string.IsNullOrEmpty(x.Name)).ToList()
                        .ForEach(x =>
                        {
                            entity.GleifEntityNames.Add(x);
                            x.Entity = entity;
                            localDbContext.GleifEntityName.Add(x);
                        });

                        new List<GleifAddress>()
                                    {
                                        csvReader.GetRecord<LegalAddress>(),
                                        csvReader.GetRecord<HeadquartersAddress>(),
                                        /*csvReader.GetRecord<OtherAddress1>(),
                                        csvReader.GetRecord<OtherAddress2>(),
                                        csvReader.GetRecord<OtherAddress3>(),
                                        csvReader.GetRecord<OtherAddress4>(),
                                        csvReader.GetRecord<OtherAddress5>(),*/
                                        /*csvReader.GetRecord<TransliteratedOtherAddress1>(),
                                        csvReader.GetRecord<TransliteratedOtherAddress2>(),
                                        csvReader.GetRecord<TransliteratedOtherAddress3>(),
                                        csvReader.GetRecord<TransliteratedOtherAddress4>(),
                                        csvReader.GetRecord<TransliteratedOtherAddress5>(),*/
                                    }
                        .Where(x => !string.IsNullOrEmpty(x.Firstaddressline)).ToList()
                        .ForEach(x =>
                        {
                            entity.GleifAddresses.Add(x);
                            x.Entity = entity;
                            localDbContext.GleifAddress.Add(x);
                        }
                        );
                        /*
                        new List<GleifValidationAuthority>()
                                    {
                                        csvReader.GetRecord<ValidationAuthority>(),
                                        csvReader.GetRecord<OtherValidationAuthority1>(),
                                        csvReader.GetRecord<OtherValidationAuthority2>(),
                                        csvReader.GetRecord<OtherValidationAuthority3>(),
                                        csvReader.GetRecord<OtherValidationAuthority4>(),
                                        csvReader.GetRecord<OtherValidationAuthority5>(),
                                    }
                        .Where(x => !string.IsNullOrEmpty(x.ValidationAuthorityId)).ToList()
                        .ForEach(x =>
                        {
                            entity.GleifValidationAuthorities.Add(x);
                            localDbContext.GleifValidationAuthority.Add(x);
                        }
                        );
                        */
                        localDbContext.GleifEntity.Add(entity);
                        counter++;
                    }
                }
                localDbContext.SaveChanges();
                localDbContext.Dispose();
                if (!canRead)
                {
                    break;
                }
            }
            total.Release(1);
        }

        private static CsvReader ConfigureCsvReader(StreamReader fileReader)
        {
            Logger.Log("Configuring CSV reader");
            var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
            csvReader.Configuration.Delimiter = ",";
            csvReader.Configuration.MissingFieldFound = null;
            csvReader.Configuration.RegisterClassMap<GleifEntityMap>();
            csvReader.Configuration.RegisterClassMap<LegalEntityNameMap>();
            csvReader.Configuration.RegisterClassMap<OtherEntityName1Map>();
            csvReader.Configuration.RegisterClassMap<OtherEntityName2Map>();
            csvReader.Configuration.RegisterClassMap<OtherEntityName3Map>();
            csvReader.Configuration.RegisterClassMap<OtherEntityName4Map>();
            csvReader.Configuration.RegisterClassMap<OtherEntityName5Map>();
            csvReader.Configuration.RegisterClassMap<TransliteratedOtherEntityName1Map>();
            csvReader.Configuration.RegisterClassMap<TransliteratedOtherEntityName2Map>();
            csvReader.Configuration.RegisterClassMap<TransliteratedOtherEntityName3Map>();
            csvReader.Configuration.RegisterClassMap<TransliteratedOtherEntityName4Map>();
            csvReader.Configuration.RegisterClassMap<TransliteratedOtherEntityName5Map>();

            csvReader.Configuration.RegisterClassMap<LegalAddressMap>();
            csvReader.Configuration.RegisterClassMap<HeadquartersAddressMap>();
            csvReader.Configuration.RegisterClassMap<OtherAddress1Map>();
            csvReader.Configuration.RegisterClassMap<OtherAddress2Map>();
            csvReader.Configuration.RegisterClassMap<OtherAddress3Map>();
            csvReader.Configuration.RegisterClassMap<OtherAddress4Map>();
            csvReader.Configuration.RegisterClassMap<OtherAddress5Map>();
            csvReader.Configuration.RegisterClassMap<TransliteratedOtherAddress1Map>();
            csvReader.Configuration.RegisterClassMap<TransliteratedOtherAddress2Map>();
            csvReader.Configuration.RegisterClassMap<TransliteratedOtherAddress3Map>();
            csvReader.Configuration.RegisterClassMap<TransliteratedOtherAddress4Map>();
            csvReader.Configuration.RegisterClassMap<TransliteratedOtherAddress5Map>();

            csvReader.Configuration.RegisterClassMap<ValidationAuthorityMap>();
            csvReader.Configuration.RegisterClassMap<OtherValidationAuthority1Map>();
            csvReader.Configuration.RegisterClassMap<OtherValidationAuthority2Map>();
            csvReader.Configuration.RegisterClassMap<OtherValidationAuthority3Map>();
            csvReader.Configuration.RegisterClassMap<OtherValidationAuthority4Map>();
            csvReader.Configuration.RegisterClassMap<OtherValidationAuthority5Map>();

            Logger.Log("CSV reader configured");
            return csvReader;
        }
    }
}
