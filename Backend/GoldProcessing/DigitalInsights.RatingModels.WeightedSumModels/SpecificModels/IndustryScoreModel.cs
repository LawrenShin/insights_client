using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels;
using System.Collections.Generic;

namespace DigitalInsights.RatingModels.WeightedSumModels
{
    internal class IndustryScoreModel : ASpecificModel
    {
        public override ScoreType ScoreType => ScoreType.IndustryScore;

        public override KeyValuePair<ScoreType, double> CalculateScore(Company company)
        {
            double result = 0;
            return new KeyValuePair<ScoreType, double>(ScoreType, result);
        }
    }
}