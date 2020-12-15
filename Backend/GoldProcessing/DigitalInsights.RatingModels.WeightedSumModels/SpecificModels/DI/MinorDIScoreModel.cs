using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.DB.Gold.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using CompanyQuestion = DigitalInsights.DB.Gold.Enums.CompanyQuestion;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.DI
{
    internal class MinorDIScoreModel : MajorDIScoreModel
    {
        public MinorDIScoreModel()
        {
            questions = new List<Tuple<CompanyQuestion, double>>()
            {
                Tuple.Create(CompanyQuestion.DIPolicyPublicallyAvailable, 0.3d),
                Tuple.Create(CompanyQuestion.DIPosition, 0.2d),
                Tuple.Create(CompanyQuestion.DIPositionFullTimePosition, 0.2d),
                Tuple.Create(CompanyQuestion.DIOfficerExecutiveMember, 0.2d),
                Tuple.Create(CompanyQuestion.DISupplyChain, 0.05d),
                Tuple.Create(CompanyQuestion.DIEarnningCall, 0.05d),
            };
        }
    }
}