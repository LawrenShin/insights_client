using DigitalInsights.DB.Gold.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Sexuality
{
    internal class CompanySexualityScoreModel : AQuestionnaireBasedModel
    {
        public override RatingType ScoreType => RatingType.SexualityScore;

        public CompanySexualityScoreModel()
        {
            questions = new List<Tuple<CompanyQuestion, double>>()
            {
                Tuple.Create(CompanyQuestion.SexualityAntiHarassmentPolicy, 0.3),
                Tuple.Create(CompanyQuestion.SexualitySupportOrientations, 0.3),
                Tuple.Create(CompanyQuestion.SexualityLBTQForum, 0.3),
                Tuple.Create(CompanyQuestion.SexualityOrientationData, 0.1),
            };
        }
    }
}
