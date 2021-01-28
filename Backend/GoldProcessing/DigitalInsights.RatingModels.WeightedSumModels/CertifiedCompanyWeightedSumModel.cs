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

        private Dictionary<RatingType, double> weights =
            new Dictionary<RatingType, double>()
            {
                { RatingType.ExecScore, 0.12 },
                { RatingType.BoardScore,0.12},
                { RatingType.DIScore, 0.1},
                { RatingType.GenderScore,0.1},
                { RatingType.RaceScore,0.1},
                { RatingType.ReligionScore,0.03},
                { RatingType.NationalityScore,0.005},
                { RatingType.UrbanScore,0.04},
                { RatingType.FamilyScore,0.02},
                { RatingType.SalaryScore,0.03},
                { RatingType.OrganizationalScore,0.04},
                { RatingType.DisabilityScore,0.02},
                { RatingType.SexualityScore,0.05},
                { RatingType.EducationScore,0.04},
                { RatingType.CountryScore,0.05},
                { RatingType.IndustryScore,0.01},
                { RatingType.PositiveSentimentScore,0.05},
                { RatingType.NegativeSentimentScore,-0.2}
            };

        public IEnumerable<ISpecificModel> RatingModels => models;

        public RatingType ScoreType => RatingType.CertifiedScore;

        public double CalculateRating(IDictionary<RatingType, double> scores)
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