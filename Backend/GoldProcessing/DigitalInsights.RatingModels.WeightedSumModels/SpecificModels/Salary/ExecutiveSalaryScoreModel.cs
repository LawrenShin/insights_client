using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Salary
{
    internal class ExecutiveSalaryScoreModel : OverallSalaryScoreModel
    {
        protected override bool IsRoleMatching(Role role)
        {
            return role.RoleType == "Executive" || role.RoleType == "Both";
        }
    }
}
