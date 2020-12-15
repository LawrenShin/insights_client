using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Gender
{
    internal class OverallGenderScoreModel : ASpecificModel
    {
        public override ScoreType ScoreType => ScoreType.GenderScore;

        protected virtual bool IsRoleMatching(Role role)
        {
            return true;
        }

        public override KeyValuePair<ScoreType, double> CalculateScore(Company company)
        {
            var roles = company.Roles.ToList();

            int male = 0;
            int female = 0;
            int other = 0;

            for (int i = 0; i < roles.Count; i++)
            {
                Person person;
                if (IsRoleMatching(roles[i]) && (person = roles[i].Person) != null)
                {
                    if (!string.IsNullOrEmpty(person.Gender))
                    {
                        if (person.Gender == "Male")
                        {
                            male++;
                        }
                        else if (person.Gender == "Female")
                        {
                            female++;
                        }
                        else
                        {
                            other++;
                        }
                    }
                }
            }

            if (male == 0 || female == 0) return new KeyValuePair<ScoreType, double>(ScoreType, 0);

            return new KeyValuePair<ScoreType, double>(ScoreType, (other > 0 ? 5d : -5d) + Math.Min(male, female) / Math.Max(male, female));
        }
    }
}