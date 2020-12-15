using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Sexuality
{
    internal class ExecutiveSexualityScoreModel : OverallSexualityScoreModel
    {
        protected override bool IsRoleMatching(Role role)
        {
            return role.RoleType == "Executive" || role.RoleType == "Both";
        }
    }
}
