using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Family
{
    internal class ExecutiveFamilyScoreModel : OverallFamilyScoreModel
    {
        public override KeyValuePair<ScoreType, double> CalculateScore(Company company)
        {
            var roles = company.Roles.ToList();

            int totalMarried = 0;
            int execMarried = 0;
            int execNotMarried = 0;
            int bothMarried = 0;
            int bothNotMarried = 0;
            int count = 0;

            for (int i = 0; i < roles.Count; i++)
            {
                Person person;
                if ((roles[i].RoleType == "Executive" || roles[i].RoleType == "Both")
                    && (person = roles[i].Person) != null)
                {
                    count++;
                    if (person.Married == "Yes") totalMarried++;

                    if (roles[i].RoleType == "Executive")
                    {
                        if (person.Married == "Yes") execMarried++;
                        if (person.Married == "No") execNotMarried++;
                    }
                    else
                    {
                        if (person.Married == "Yes") bothMarried++;
                        if (person.Married == "No") bothNotMarried++;
                    }

                }
            }

            if (count == 0) new KeyValuePair<ScoreType, double>(ScoreType, 0);

            return new KeyValuePair<ScoreType, double>(ScoreType, (totalMarried > 0 ? 50d : 0) +
                (totalMarried == 0 ? 
                    0d :
                    (execMarried + bothMarried > 1) || (execNotMarried + bothNotMarried > 1) ?
                        0d : 
                        25d));
        }
    }
}
