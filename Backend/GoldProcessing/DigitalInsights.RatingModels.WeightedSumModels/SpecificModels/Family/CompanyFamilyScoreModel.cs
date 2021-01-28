using DigitalInsights.DB.Gold.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Family
{
    internal class CompanyFamilyScoreModel : AQuestionnaireBasedModel
    {
        public override RatingType ScoreType => RatingType.FamilyScore;

        public CompanyFamilyScoreModel()
        {
            questions = new List<Tuple<CompanyQuestion, double>>()
            {
                Tuple.Create(CompanyQuestion.FamilyPaidParentalLeave, 0.4),
                Tuple.Create(CompanyQuestion.FamilyParentalLeavePolicyThreeMonths, 0.2),
                Tuple.Create(CompanyQuestion.FamilyPaternityLeaveSameAsMaternity, 0.4),
            };
        }
    }
}
