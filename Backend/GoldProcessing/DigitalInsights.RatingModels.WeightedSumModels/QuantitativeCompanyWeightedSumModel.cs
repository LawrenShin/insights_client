using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.DI;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Disability;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Education;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Family;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Gender;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Nationality;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Race;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Religion;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Salary;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Sentiment;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Sexuality;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Urban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels
{
    public class QuantitativeCompanyWeightedSumModel : IGeneralModel
    {
        private List<ISpecificModel> models =
            new List<ISpecificModel>()
            {
                { new ExecScoreModel() },
                { new BoardScoreModel()},
                { new MinorDIScoreModel()},
                { new CountryScoreModel()},
                { new IndustryScoreModel()},
                { new PositiveSentimentScoreModel()},
                { new NegativeSentimentScoreModel()},
            };

        private Dictionary<ScoreType, double> weights =
            new Dictionary<ScoreType, double>()
            {
                { ScoreType.ExecScore, 0.3 },
                { ScoreType.BoardScore,0.2 },
                { ScoreType.DIScore, 0.1 },
                { ScoreType.CountryScore, 0.15 },
                { ScoreType.IndustryScore, 0.05 },
                { ScoreType.PositiveSentimentScore, 0.05 },
                { ScoreType.NegativeSentimentScore, -0.15 }
            };

        public IEnumerable<ISpecificModel> RatingModels => models;

        public ScoreType ScoreType => ScoreType.QuantitativeScore;

        public double CalculateRating(IDictionary<ScoreType, double> scores)
        {
            if (!weights.All(x => scores.ContainsKey(x.Key)))
            {
                throw new InvalidOperationException("Not every score was provided for the total company model!");
            }

            return weights.Sum(x => x.Value * scores[x.Key]);
        }
        public double CalculateRating(Company company)
        {
            return CalculateRating(models.Select(x => x.CalculateScore(company)).ToDictionary(x => x.Key, x => x.Value));
        }
    }
}