using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.DB.Gold.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyQuestion = DigitalInsights.DB.Gold.Enums.CompanyQuestion;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Organization
{
    internal class CompanyOrganizationalScoreModel : AQuestionnaireBasedModel
    {
        public override ScoreType ScoreType => ScoreType.OrganizationalScore;

        public CompanyOrganizationalScoreModel()
        {
            questions = new List<Tuple<CompanyQuestion, double>>()
            {
                Tuple.Create(CompanyQuestion.OrganizationalCompanyIntranet, 0.25),
                Tuple.Create(CompanyQuestion.OrganizationalCompanyTownHalls, 0.25),
                Tuple.Create(CompanyQuestion.OrganizationalStructurePublished, 0.2),
            };
        }

        public override KeyValuePair<ScoreType, double> CalculateScore(Company company)
        {
            var baseResult = base.CalculateScore(company);

            var extendedData = company.CompanyExtendedData.FirstOrDefault();
            if (extendedData == null) return baseResult;

            var hierarchyLevel = extendedData.HierarchyLevel;
            var hierarchyScore = 0;
            if (hierarchyLevel.HasValue)
            {
                hierarchyScore =
                    hierarchyLevel <= 2
                    ? 100
                    : hierarchyLevel <= 5
                        ? 80
                        : hierarchyLevel <= 10
                            ? 60
                            : hierarchyLevel <= 15
                                ? 30
                                : 0;
            }
            return new KeyValuePair<ScoreType, double>(ScoreType, baseResult.Value + 0.3 * hierarchyScore);
        }
    }
}
