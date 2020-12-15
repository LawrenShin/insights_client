using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels;
using System.Collections.Generic;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Urban
{
    internal class OverallUrbanScoreModel : ASpecificModel
    {
        public override ScoreType ScoreType => ScoreType.UrbanScore;

        public override KeyValuePair<ScoreType, double> CalculateScore(Company company)
        {
            double result = 0;
            return new KeyValuePair<ScoreType, double>(ScoreType, result);
        }
    }
}