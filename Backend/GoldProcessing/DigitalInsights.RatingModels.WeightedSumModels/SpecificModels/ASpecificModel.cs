using DigitalInsights.DB.Gold;
using DigitalInsights.DB.Gold.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels
{
    internal abstract class ASpecificModel : ISpecificModel
    {
        protected static readonly Dictionary<string, Country> countries;

        protected static readonly GoldContext dbContext = new GoldContext();

        static ASpecificModel()
        {
            countries = dbContext.Country.AsNoTracking()
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
                    .Include(x => x.CountryUrbans).ToDictionary(x => x.Code, x => x);
        }

        public abstract ScoreType ScoreType { get; }

        public abstract KeyValuePair<ScoreType, double> CalculateScore(Company company);
    }
}
