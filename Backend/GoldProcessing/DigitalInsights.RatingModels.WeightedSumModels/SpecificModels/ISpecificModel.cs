using System;
using System.Collections.Generic;
using System.Text;

using DigitalInsights.DB.Gold.Entities;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels
{
    public interface ISpecificModel
    {
        public RatingType ScoreType { get; }

        public KeyValuePair<RatingType, double> CalculateScore(Company company);
    }
}
