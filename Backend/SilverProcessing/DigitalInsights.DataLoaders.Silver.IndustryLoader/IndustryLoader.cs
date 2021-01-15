using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

using Amazon.Lambda.Core;
using CsvHelper;
using DigitalInsights.Common.Logging;
using DigitalInsights.DataLoaders.Silver.IndustryLoader.Model.CSV;
using DigitalInsights.DB.Silver;
using DigitalInsights.DB.Silver.Entities;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DigitalInsights.DataLoaders.Silver.IndustryLoader
{
    public class IndustryLoader
    {
        const string TRANSFORMER_NAME = "INDUSTRY LOADER";

        /// <summary>
        /// Loader of industries
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
                var filename = "C:\\temp\\industry.csv";

                Logger.Log("Configuring DB context.");
                SilverContext dbContext = new SilverContext();
                dbContext.ChangeTracker.LazyLoadingEnabled = true;

                var industries = dbContext.Industries.ToList();

                using (var fileReader = new StreamReader(filename))
                {
                    Logger.Log("Configuring CSV reader");
                    var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                    csvReader.Configuration.Delimiter = ";";
                    csvReader.Configuration.MissingFieldFound = null;
                    csvReader.Configuration.RegisterClassMap<IndustryMap>();
                    csvReader.Configuration.RegisterClassMap<IndustryCountryMap>();

                    while (csvReader.Read())
                    {
                        var industry = csvReader.GetRecord<Industry>();

                        var targetIndustry = industries.First(x=>x.Name == industry.Name.Trim());

                        var industryCountry = csvReader.GetRecord<IndustryCountry>();
                        industryCountry.Industry = (DB.Common.Enums.Industry)targetIndustry.Id;

                        dbContext.Add(industryCountry);
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
