using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels;
using System.Collections.Generic;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Urban
{
    internal class OverallUrbanScoreModel : ASpecificModel
    {
        public override RatingType ScoreType => RatingType.UrbanScore;

        public override KeyValuePair<RatingType, double> CalculateScore(Company company)
        {
            double result = 0;
            return new KeyValuePair<RatingType, double>(ScoreType, result);
        }
    }
}