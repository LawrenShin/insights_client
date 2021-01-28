using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.DB.Gold.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Disability
{
    internal class CompanyDisabilityScoreModel : ASpecificModel
    {
        public CompanyDisabilityScoreModel()
        {
        }

        public override RatingType ScoreType => RatingType.DisabilityScore;

        public override KeyValuePair<RatingType, double> CalculateScore(Company company)
        {
            var wheelchairWeight = 0;
            var disabledWeight = 0d;

            var companyQuestion = company.CompanyQuestionnaires.Where(x => x.Question == DB.Gold.Enums.CompanyQuestion.DisabilityWheelchairAccessible).FirstOrDefault();

            if (companyQuestion != null)
            {
                wheelchairWeight = companyQuestion.Answer;
            }

            var companyExtendedData = company.CompanyExtendedData.FirstOrDefault();
            if (companyExtendedData != null && companyExtendedData.DisabledEmployees.HasValue)
            {
                var companyPercentage = companyExtendedData.DisabledEmployees.Value;
                var code = company.LegalJurisdiction.Split('-')[0];
                if (!countries.ContainsKey(code))
                {
                    return new KeyValuePair<RatingType, double>(ScoreType, 0);
                }
                var country = countries[code];
                var countryPercentage = country.CountryDisabilities.First().Disabled;

                disabledWeight = Math.Min(companyPercentage, countryPercentage) / Math.Max(companyPercentage, countryPercentage) * 100d;
            }

            return new KeyValuePair<RatingType, double>(ScoreType,
                0.7 * wheelchairWeight + 0.3 * disabledWeight);
        }
    }
}
