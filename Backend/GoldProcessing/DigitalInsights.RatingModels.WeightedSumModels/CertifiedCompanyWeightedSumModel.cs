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
    public class CertifiedCompanyWeightedSumModel : IGeneralModel
    {
        private List<ISpecificModel> models =
            new List<ISpecificModel>()
            {
                { new ExecScoreModel() },
                { new BoardScoreModel()},
                { new MajorDIScoreModel()},
                { new OverallGenderScoreModel()},
                { new OverallRaceScoreModel()},
                { new OverallReligionScoreModel()},
                { new OverallNationalityScoreModel()},
                { new OverallUrbanScoreModel()},
                { new CompanyFamilyScoreModel()},
                { new CompanySalaryScoreModel()},
                { new OrganizationalScoreModel()},
                { new CompanyDisabilityScoreModel()},
                { new CompanySexualityScoreModel()},
                { new CompanyEducationScoreModel()},
                { new CountryScoreModel()},
                { new IndustryScoreModel()},
                { new PositiveSentimentScoreModel()},
                { new NegativeSentimentScoreModel()},
            };

        private Dictionary<ScoreType, double> weights =
            new Dictionary<ScoreType, double>()
            {
                { ScoreType.ExecScore, 0.12 },
                { ScoreType.BoardScore,0.12},
                { ScoreType.DIScore, 0.1},
                { ScoreType.GenderScore,0.1},
                { ScoreType.RaceScore,0.1},
                { ScoreType.ReligionScore,0.03},
                { ScoreType.NationalityScore,0.005},
                { ScoreType.UrbanScore,0.04},
                { ScoreType.FamilyScore,0.02},
                { ScoreType.SalaryScore,0.03},
                { ScoreType.OrganizationalScore,0.04},
                { ScoreType.DisabilityScore,0.02},
                { ScoreType.SexualityScore,0.05},
                { ScoreType.EducationScore,0.04},
                { ScoreType.CountryScore,0.05},
                { ScoreType.IndustryScore,0.01},
                { ScoreType.PositiveSentimentScore,0.05},
                { ScoreType.NegativeSentimentScore,-0.2}
            };

        public IEnumerable<ISpecificModel> RatingModels => models;

        public ScoreType ScoreType => ScoreType.CertifiedScore;

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
            return CalculateRating(models.Select(x => x.CalculateScore(company)).ToDictionary(x=>x.Key, x=>x.Value));
        }
    }
}