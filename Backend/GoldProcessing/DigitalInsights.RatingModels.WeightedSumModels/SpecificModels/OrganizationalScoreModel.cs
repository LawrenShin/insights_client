using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels;
using System.Collections.Generic;

namespace DigitalInsights.RatingModels.WeightedSumModels
{
    internal class OrganizationalScoreModel : ASpecificModel
    {
        public override ScoreType ScoreType => ScoreType.OrganizationalScore;

        public override KeyValuePair<ScoreType, double> CalculateScore(Company company)
        {
            double result = 0;
            return new KeyValuePair<ScoreType, double>(ScoreType, result);
        }
    }
}