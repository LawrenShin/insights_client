using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels;
using System;
using System.Collections.Generic;

namespace DigitalInsights.RatingModels.WeightedSumModels
{
    public interface IGeneralModel
    {
        public ScoreType ScoreType { get; }

        public double CalculateRating(IDictionary<ScoreType, double> scores);

        public double CalculateRating(Company company);

        public IEnumerable<ISpecificModel> RatingModels { get; }
    }
}
