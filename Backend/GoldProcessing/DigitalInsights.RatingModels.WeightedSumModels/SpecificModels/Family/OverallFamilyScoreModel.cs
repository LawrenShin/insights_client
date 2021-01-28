using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Family
{
    internal class OverallFamilyScoreModel : ASpecificModel
    {
        public override RatingType ScoreType => RatingType.FamilyScore;

        public override KeyValuePair<RatingType, double> CalculateScore(Company company)
        {
            return  new KeyValuePair<RatingType, double>(ScoreType, 0);
        }
    }
}