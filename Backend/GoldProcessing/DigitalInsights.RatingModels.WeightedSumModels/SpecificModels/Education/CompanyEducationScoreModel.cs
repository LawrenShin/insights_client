using DigitalInsights.DB.Gold.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Education
{
    internal class CompanyEducationScoreModel : AQuestionnaireBasedModel
    {
        public override ScoreType ScoreType => ScoreType.EducationScore;

        public CompanyEducationScoreModel()
        {
            questions = new List<Tuple<CompanyQuestion, double>>()
            {
                Tuple.Create(CompanyQuestion.EducationContinuedEducationLeave, 0.4),
                Tuple.Create(CompanyQuestion.EducationFinancialEducationalSupport, 0.3),
                Tuple.Create(CompanyQuestion.EducationStudentDebtAssistance, 0.3),
            };
        }
    }
}
