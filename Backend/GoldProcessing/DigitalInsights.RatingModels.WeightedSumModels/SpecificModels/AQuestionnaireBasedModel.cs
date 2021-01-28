using DigitalInsights.DB.Gold;
using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.DB.Gold.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels
{
    internal abstract class AQuestionnaireBasedModel : ASpecificModel
    {
        protected List<Tuple<DB.Gold.Enums.CompanyQuestion, double>> questions;

        public AQuestionnaireBasedModel()
        {
            questions = new List<Tuple<DB.Gold.Enums.CompanyQuestion, double>>()
            {
            };
        }

        public override KeyValuePair<RatingType, double> CalculateScore(Company company)
        {
            var answers = company.CompanyQuestionnaires.ToDictionary(x => x.Question, x => x.Answer);

            return new KeyValuePair<RatingType, double>(ScoreType,
                questions.Select(x => x.Item2 * (answers.ContainsKey(x.Item1) ? answers[x.Item1] : 0)).Sum());
        }
    }
}
