using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Age
{
    internal class OverallAgeScoreModel : ASpecificModel
    {
        public override RatingType ScoreType => RatingType.AgeScore;

        protected virtual bool IsRoleMatching(Role role)
        {
            return true;
        }

        public override KeyValuePair<RatingType, double> CalculateScore(Company company)
        {
            var roles = company.Roles.ToList();

            int total = 0;
            int count = 0;
            int year = DateTime.Now.Year;

            for (int i = 0; i < roles.Count; i++)
            {
                Person person;
                if (IsRoleMatching(roles[i]) && (person = roles[i].Person) != null)
                {
                    count++;
                    total += person.Age ?? 0;
                }
            }

            if (count == 0) return new KeyValuePair<RatingType, double>(ScoreType, 0);

            double averageAge = (double)total / count;

            var code = company.LegalJurisdiction.Split('-')[0];
            if (!countries.ContainsKey(code))
            {
                return new KeyValuePair<RatingType, double>(ScoreType, 0);
            }
            var country = countries[code];
            var countryAge = country.CountryAges.First().Avg18;

            return new KeyValuePair<RatingType, double>(ScoreType, Math.Min(averageAge, countryAge) / Math.Max(averageAge, countryAge));
        }
    }
}