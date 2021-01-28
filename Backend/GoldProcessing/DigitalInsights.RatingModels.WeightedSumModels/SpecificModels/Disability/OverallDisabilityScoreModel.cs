using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Disability
{
    internal class OverallDisabilityScoreModel : ASpecificModel
    {
        public override RatingType ScoreType => RatingType.DisabilityScore;

        protected virtual bool IsRoleMatching(Role role)
        {
            return true;
        }

        public override KeyValuePair<RatingType, double> CalculateScore(Company company)
        {
            var roles = company.Roles.ToList();

            int disabled = 0;
            int notDisabled = 0;

            int count = 0;

            for (int i = 0; i < roles.Count; i++)
            {
                Person person;
                if (IsRoleMatching(roles[i]) && (person = roles[i].Person) != null)
                {
                    if (!string.IsNullOrEmpty(person.Disability))
                    {
                        count++;
                        if (person.Disability == "Yes") disabled++;
                        else if (person.Disability == "No") notDisabled++;
                    }
                }
            }

            if (count == 0) new KeyValuePair<RatingType, double>(ScoreType, 0);

            return new KeyValuePair<RatingType, double>(ScoreType,
                (count > 0 ? 50d : 0) +
                (count == 0 ? 0 : (disabled > 1 || notDisabled > 1) ? 0 : 50d));
        }
    }
}