using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Family
{
    internal class OverallFamilyScoreModel : ASpecificModel
    {
        public override ScoreType ScoreType => ScoreType.FamilyScore;

        public override KeyValuePair<ScoreType, double> CalculateScore(Company company)
        {
            return  new KeyValuePair<ScoreType, double>(ScoreType, 0);
        }
    }
}