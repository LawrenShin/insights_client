using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels;
using System;
using System.Collections.Generic;

namespace DigitalInsights.RatingModels.WeightedSumModels
{
    public interface IGeneralModel
    {
        public RatingType ScoreType { get; }

        public double CalculateRating(IDictionary<RatingType, double> scores);

        public double CalculateRating(Company company);

        public IEnumerable<ISpecificModel> RatingModels { get; }
    }
}
