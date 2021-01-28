using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Religion
{
    internal class OverallReligionScoreModel : ASpecificModel
    {
        public override RatingType ScoreType => RatingType.ReligionScore;

        protected virtual bool IsRoleMatching(Role role)
        {
            return true;
        }

        public override KeyValuePair<RatingType, double> CalculateScore(Company company)
        {
            var roles = company.Roles.ToList();

            int muslim = 0;
            int christian = 0;
            int hindu = 0;
            int buddhism = 0;
            int judaism = 0;
            int other = 0;

            int count = 0;

            for (int i = 0; i < roles.Count; i++)
            {
                Person person;
                if (IsRoleMatching(roles[i]) && (person = roles[i].Person) != null)
                {
                    if (!string.IsNullOrEmpty(person.Religion)) count++;
                    if (person.Religion == "Muslim") muslim++; // no religion in data
                    else if (person.Religion == "Christian") christian++;
                    else if (person.Religion == "Hindu") hindu++;
                    else if (person.Religion == "Buddhism") buddhism++;
                    else if (person.Religion == "Judaism") judaism++;
                    else if (person.Religion == "Other") other++;
                }
            }

            if (count == 0) new KeyValuePair<RatingType, double>(ScoreType, 0);

            var code = company.LegalJurisdiction.Split('-')[0];
            if (!countries.ContainsKey(code))
            {
                return new KeyValuePair<RatingType, double>(ScoreType, 0);
            }
            var country = countries[code];
            var countryReligion = country.CountryReligions.First();

            var specificReligionScores = new List<double>()
            {
                Math.Min((double)muslim / count, countryReligion.Muslim) / Math.Max((double)muslim / count, countryReligion.Muslim),
                Math.Min((double)christian / count, countryReligion.Christian) / Math.Max((double)christian / count, countryReligion.Christian),
                Math.Min((double)hindu / count, countryReligion.Hindu) / Math.Max((double)hindu / count, countryReligion.Hindu),
                Math.Min((double)buddhism / count, countryReligion.Buddishm) / Math.Max((double)buddhism / count, countryReligion.Buddishm),
                Math.Min((double)judaism / count, countryReligion.Judaism) / Math.Max((double)judaism / count, countryReligion.Judaism),
                Math.Min((double)other / count, countryReligion.Other) / Math.Max((double)other / count, countryReligion.Other),
            };

            var moreThanZero = specificReligionScores.Where(x => x > 0);

            if(!moreThanZero.Any())
            {
                return new KeyValuePair<RatingType, double>(ScoreType, 0);
            }

            return new KeyValuePair<RatingType, double>(ScoreType, specificReligionScores.Where(x => x > 0).Average());
        }
    }
}