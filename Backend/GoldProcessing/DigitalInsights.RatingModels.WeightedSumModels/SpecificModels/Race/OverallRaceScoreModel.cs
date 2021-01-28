using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Race
{
    internal class OverallRaceScoreModel : ASpecificModel
    {
        public override RatingType ScoreType => RatingType.RaceScore;

        protected virtual bool IsRoleMatching(Role role)
        {
            return true;
        }

        public override KeyValuePair<RatingType, double> CalculateScore(Company company)
        {
            var roles = company.Roles.ToList();

            int black = 0;
            int asian = 0;
            int hispanic = 0;
            int arab = 0;
            int caucasian = 0;
            int indigenious = 0;

            int count = 0;

            for (int i = 0; i < roles.Count; i++)
            {
                Person person;
                if (IsRoleMatching(roles[i]) && (person = roles[i].Person) != null)
                {
                    if (!string.IsNullOrEmpty(person.Race)) count++;
                    if (person.Race == "Black") black++;
                    else if (person.Race == "Asian") asian++;
                    else if (person.Race == "Hispanic") hispanic++;
                    else if (person.Race == "Arab") arab++;
                    else if (person.Race == "Caucasian") caucasian++;
                    else if (person.Race == "Indigenious") indigenious++;
                }
            }

            if (count == 0)
                return new KeyValuePair<RatingType, double>(ScoreType, 0);

            var code = company.LegalJurisdiction.Split('-')[0];
            if (!countries.ContainsKey(code))
            {
                return new KeyValuePair<RatingType, double>(ScoreType, 0);
            }
            var country = countries[code];
            var countryRace = country.CountryRaces.First();

            var specificRaceScores = new List<double>()
            {
                Math.Min((double)black / count, countryRace.Black) / Math.Max((double)black / count, countryRace.Black),
                Math.Min((double)asian / count, countryRace.Asian) / Math.Max((double)asian / count, countryRace.Asian),
                Math.Min((double)hispanic / count, countryRace.Hispanic) / Math.Max((double)hispanic / count, countryRace.Hispanic),
                Math.Min((double)arab / count, countryRace.Arab) / Math.Max((double)arab / count, countryRace.Arab),
                Math.Min((double)caucasian / count, countryRace.Caucasian) / Math.Max((double)caucasian / count, countryRace.Caucasian),
                Math.Min((double)indigenious / count, countryRace.Indegineous) / Math.Max((double)indigenious / count, countryRace.Indegineous),
            };

            return new KeyValuePair<RatingType, double>(ScoreType, specificRaceScores.Where(x => x > 0).Average());
        }
    }
}