using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Nationality
{
    internal class ExecutiveNationalityScoreModel : OverallNationalityScoreModel
    {
        protected override bool IsRoleMatching(Role role)
        {
            return role.RoleType == "Executive" || role.RoleType == "Both";
        }
    }
}
