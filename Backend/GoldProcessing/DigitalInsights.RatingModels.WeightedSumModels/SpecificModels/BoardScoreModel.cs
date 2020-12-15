using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Age;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Disability;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Education;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Family;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Gender;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Nationality;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Race;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Religion;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Salary;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Sexuality;
using DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Urban;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.RatingModels.WeightedSumModels
{
    internal class BoardScoreModel : ASpecificModel
    {
        const double GENDER_WEIGHT = 0.3d;
        const double AGE_WEIGHT = 0.05d;
        const double EDUCATION_WEIGHT = 0.07d;
        const double FAMILY_WEIGHT = 0.05d;
        const double SEXUALITY_WEIGHT = 0.05d;
        const double RELIGION_WEIGHT = 0.15d;
        const double RACE_WEIGHT = 0.2d;
        const double NATIONALITY_WEIGHT = 0.1d;
        const double URBAN_WEIGHT = 0.01d;
        const double SALARY_WEIGHT = 0.01d;
        const double DISABILITY_WEIGHT = 0.01d;

        List<Tuple<ASpecificModel, double>> models = new List<Tuple<ASpecificModel, double>>()
        {
            Tuple.Create<ASpecificModel, double>(new BoardGenderScoreModel(), GENDER_WEIGHT),
            Tuple.Create<ASpecificModel, double>(new BoardReligionScoreModel(), RELIGION_WEIGHT),
            Tuple.Create<ASpecificModel, double>(new BoardSexualityScoreModel(), SEXUALITY_WEIGHT),
            Tuple.Create<ASpecificModel, double>(new BoardRaceScoreModel(), RACE_WEIGHT),
            Tuple.Create<ASpecificModel, double>(new BoardEducationScoreModel(), EDUCATION_WEIGHT),
            Tuple.Create<ASpecificModel, double>(new BoardAgeScoreModel(), AGE_WEIGHT),
            Tuple.Create<ASpecificModel, double>(new BoardFamilyScoreModel(), FAMILY_WEIGHT),
            Tuple.Create<ASpecificModel, double>(new BoardNationalityScoreModel(), NATIONALITY_WEIGHT),
            Tuple.Create<ASpecificModel, double>(new BoardUrbanScoreModel(), URBAN_WEIGHT),
            Tuple.Create<ASpecificModel, double>(new BoardSalaryScoreModel(), SALARY_WEIGHT),
            Tuple.Create<ASpecificModel, double>(new BoardDisabilityScoreModel(), DISABILITY_WEIGHT),
        };

        public override ScoreType ScoreType => ScoreType.BoardScore;

        public override KeyValuePair<ScoreType, double> CalculateScore(Company company)
        {
            return new KeyValuePair<ScoreType, double>(ScoreType,
                models.Aggregate(0.0d, (x, y) => x + y.Item1.CalculateScore(company).Value * y.Item2));
        }
    }
}