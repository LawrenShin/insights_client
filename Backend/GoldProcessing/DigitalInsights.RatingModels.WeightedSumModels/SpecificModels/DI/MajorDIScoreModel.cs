using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.DB.Gold.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using CompanyQuestion = DigitalInsights.DB.Gold.Enums.CompanyQuestion;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.DI
{
    internal class MajorDIScoreModel : AQuestionnaireBasedModel
    {
        public MajorDIScoreModel()
        {
            questions = new List<Tuple<CompanyQuestion, double>>()
            {
                Tuple.Create(CompanyQuestion.DIPolicyEstablished, 0.1d),
                Tuple.Create(CompanyQuestion.DIPolicyPublicallyAvailable, 0.08d),
                Tuple.Create(CompanyQuestion.DIPosition, 0.1d),
                Tuple.Create(CompanyQuestion.DIPositionFullTimePosition, 0.12d),
                Tuple.Create(CompanyQuestion.DIOfficerExecutiveMember, 0.1d),
                Tuple.Create(CompanyQuestion.DIHROrCompliance, 0.05d),
                Tuple.Create(CompanyQuestion.DICodeofConduct, 0.05d),
                Tuple.Create(CompanyQuestion.DIManagingDiverse, 0.1d),
                Tuple.Create(CompanyQuestion.DIComplaintBox, 0.05d),
                Tuple.Create(CompanyQuestion.DISupplyChain, 0.05d),
                Tuple.Create(CompanyQuestion.DITalentGoals, 0.1d),
                Tuple.Create(CompanyQuestion.DIEarnningCall, 0.1d),
            };
        }

        public override RatingType ScoreType => RatingType.DIScore;
    }
}