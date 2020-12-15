using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Religion
{
    internal class BoardReligionScoreModel : OverallReligionScoreModel
    {
        protected override bool IsRoleMatching(Role role)
        {
            return role.RoleType == "Board" || role.RoleType == "Both";
        }
    }
}
