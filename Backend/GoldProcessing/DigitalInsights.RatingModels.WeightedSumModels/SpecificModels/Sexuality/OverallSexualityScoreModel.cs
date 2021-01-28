using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Sexuality
{
    internal class OverallSexualityScoreModel : ASpecificModel
    {
        public override RatingType ScoreType => RatingType.SexualityScore;

        protected virtual bool IsRoleMatching(Role role)
        {
            return true;
        }

        public override KeyValuePair<RatingType, double> CalculateScore(Company company)
        {
            var roles = company.Roles.ToList();

            int totalReported = 0;
            int totalLBGTQ = 0;
            int count = 0;

            for (int i = 0; i < roles.Count; i++)
            {
                Person person;
                if (IsRoleMatching(roles[i]) && (person = roles[i].Person) != null)
                {
                    count++;
                    if (!string.IsNullOrEmpty(person.Sexuality)) totalReported++;

                    if (person.Sexuality == "LBGTQ") totalLBGTQ++;
                }
            }

            if (count == 0) return new KeyValuePair<RatingType, double>(ScoreType, 0);

            double percentageLBGTQ = ((double)totalLBGTQ) / count;

            return new KeyValuePair<RatingType, double>(ScoreType, 
                (totalReported > 0 ? 50d : 0) +
                (percentageLBGTQ > 0.1d && percentageLBGTQ < 0.15 ? 50d : 0));
        }
    }
}
