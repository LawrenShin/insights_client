using System;
using System.Collections.Generic;
using System.Text;

using DigitalInsights.DB.Gold.Entities;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels
{
    public interface ISpecificModel
    {
        public ScoreType ScoreType { get; }

        public KeyValuePair<ScoreType, double> CalculateScore(Company company);
    }
}
