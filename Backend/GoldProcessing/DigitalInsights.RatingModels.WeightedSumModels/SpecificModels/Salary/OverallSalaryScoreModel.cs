using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Salary
{
    internal class OverallSalaryScoreModel : ASpecificModel
    {
        public override ScoreType ScoreType => ScoreType.SalaryScore;

        protected virtual bool IsRoleMatching(Role role)
        {
            return true;
        }

        public override KeyValuePair<ScoreType, double> CalculateScore(Company company)
        {
            var roles = company.Roles.ToList();

            int count = 0;
            double salarySum = 0;

            for (int i = 0; i < roles.Count; i++)
            {
                Person person;
                if (IsRoleMatching(roles[i]) && (person = roles[i].Person) != null)
                {
                    if (person.BaseSalary.HasValue)
                    {
                        count++;
                        salarySum = person.BaseSalary.Value;
                    }
                }
            }

            if (count == 0) new KeyValuePair<ScoreType, double>(ScoreType, 0);

            var code = company.LegalJurisdiction.Split('-')[0];
            if (!countries.ContainsKey(code))
            {
                return new KeyValuePair<ScoreType, double>(ScoreType, 0);
            }
            var country = countries[code];
            var countryAvgSalary = country.CountryEconomies.First().AvgIncome;

            // minor optimization, equals to salarySum / count < countryAvgSalary * 2
            return new KeyValuePair<ScoreType, double>(ScoreType, salarySum < countryAvgSalary * 2 * count ? 100 : 0);
        }
    }
}