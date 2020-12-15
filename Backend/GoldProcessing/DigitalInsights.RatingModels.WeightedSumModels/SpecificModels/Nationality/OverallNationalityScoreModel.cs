using DigitalInsights.DB.Gold.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Nationality
{
    internal class OverallNationalityScoreModel : ASpecificModel
    {
        public override ScoreType ScoreType => ScoreType.NationalityScore;

        protected virtual bool IsRoleMatching(Role role)
        {
            return true;
        }

        public override KeyValuePair<ScoreType, double> CalculateScore(Company company)
        {
            var roles = company.Roles.ToList();

            Dictionary<string, int> nations = new Dictionary<string, int>();

            var code = company.LegalJurisdiction.Split('-')[0];
            if (!countries.ContainsKey(code))
            {
                return new KeyValuePair<ScoreType, double>(ScoreType, 0);
            }
            var country = countries[code];

            int count = 0;
            int inCount = 0;

            for (int i = 0; i < roles.Count; i++)
            {
                Person person;
                if (IsRoleMatching(roles[i]) && (person = roles[i].Person) != null)
                {
                    if (person.PersonCountries.Count > 0)
                    {
                        count++;
                        var countries = person.PersonCountries.ToList();
                        for (int j=0;j< countries.Count;j++)
                        {
                            if (countries[j].CountryId == country.Id)
                            {
                                inCount++;
                                break;
                            }
                        }
                    }
                }
            }

            if (count == 0) return new KeyValuePair<ScoreType, double>(ScoreType, 0);

            return new KeyValuePair<ScoreType, double>(ScoreType, 100d * inCount / count);
        }
    }
}