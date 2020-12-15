using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Religion
{
    internal class ExecutiveReligionScoreModel : OverallReligionScoreModel
    {
        protected override bool IsRoleMatching(Role role)
        {
            return role.RoleType == "Executive" || role.RoleType == "Both";
        }
    }
}
