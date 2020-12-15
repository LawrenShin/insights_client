using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using DigitalInsights.Common.Logging;
using DigitalInsights.DB.Gold;
using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.RatingModels.WeightedSumModels;
using DigitalInsights.Transformers.GLEIFLoader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DigitalInsights.RatingCalculators.GoldRatingCalculator
{
    public class GoldRatingCalculator
    {
        const string CALCULATOR_NAME = "GOLD RATING CALCULATOR";

        const int PAGE_SIZE = 10000;

        //Semaphore semaphore = new Semaphore(0, THREAD_COUNT);

        /// <summary>
        /// A function to calculate all ratings for Gold data snapshot
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public void FunctionHandler(ILambdaContext context)
        {
            try
            {
                Logger.Init(CALCULATOR_NAME);
                Logger.Log("Started");

                var factory = LoggerFactory.Create(builder =>
                {
                    builder.AddProvider(new TraceLoggerProvider());
                });
                var dbContext = new GoldContext(factory);

                var companiesCount = dbContext.Company.Count(x=>true);

                var pages = Enumerable.Range(0, (int)Math.Ceiling((double)companiesCount / PAGE_SIZE)).ToList();

                pages
                    .AsParallel()
                    .WithDegreeOfParallelism(24)
                    .ForAll(x => CalculateChunk(x));
                    //.ForEach(x => CalculateChunk(x));
            }
            catch(Exception ex)
            {
                Logger.Log($"ERROR: Unhandled exception: {ex}");
            }
        }

        public void CalculateChunk(int page)
        { 

            var goldContext = new GoldContext();
            //goldContext.ChangeTracker.AutoDetectChangesEnabled = false;
            goldContext.ChangeTracker.LazyLoadingEnabled = true;
            //goldContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            Logger.Log($"Statrting to load page {page} of companies");

            var companies = goldContext.Company.AsNoTracking()
                                                 //.Include(x => x.Hq)
                                                 //.Include(x => x.Legal)
                .Include(x => x.CompanyNames)
                .Include(x => x.CompanyQuestionnaires)
                .Include(x => x.Roles).ThenInclude(x => x.Person).ThenInclude(x => x.PersonCountries)
                .Skip(page * PAGE_SIZE).Take(PAGE_SIZE).ToList();

            var models = new IGeneralModel[] 
            { 
                new CertifiedCompanyWeightedSumModel(), 
                new QuantitativeCompanyWeightedSumModel() 
            };

            foreach(var company in companies)
            {
                var scores = models.Select(x => new { x.ScoreType, Rating = x.CalculateRating(company) });

                goldContext.AddRange(scores.Select(x =>
                    new Ratings()
                    {
                        CompanyId = company.Id,
                        RatingType = (int)x.ScoreType,
                        RatingValue = x.Rating
                    }));
            }

            goldContext.SaveChanges();
            goldContext.Dispose();
        }
    }
}
