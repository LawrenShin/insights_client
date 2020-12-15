using DigitalInsights.DB.Gold.Entities;
using DigitalInsights.DB.Gold.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Salary
{
    internal class CompanySalaryScoreModel : ExecutiveSalaryScoreModel
    { 
        public CompanySalaryScoreModel()
        {
        }

        public override KeyValuePair<ScoreType, double> CalculateScore(Company company)
        {
            var companyExtendedData = company.CompanyExtendedData.FirstOrDefault();
            if (companyExtendedData == null) return new KeyValuePair<ScoreType, double>(ScoreType, 0);

            double execSalaryCoefficient = 0;

            if (companyExtendedData.MedianSalary.HasValue)
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

                if (count > 0)
                {
                    execSalaryCoefficient = companyExtendedData.MedianSalary > (salarySum / count) ? 100 : 0;
                }
            }

            return new KeyValuePair<ScoreType, double>(ScoreType,
                (companyExtendedData.RetentionRate.HasValue 
                    ? companyExtendedData.RetentionRate.Value * 0.7 
                    : 0) +
                0.3 * execSalaryCoefficient +
                (companyExtendedData.BelowNationalAvgIncome.HasValue 
                    ? companyExtendedData.BelowNationalAvgIncome.Value * 0.1 
                    : 0));
        }
    }
}
