using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Disability
{
    internal class ExecutiveDisabilityScoreModel : OverallDisabilityScoreModel
    {
        protected override bool IsRoleMatching(Role role)
        {
            return role.RoleType == "Executive" || role.RoleType == "Both";
        }
    }
}
