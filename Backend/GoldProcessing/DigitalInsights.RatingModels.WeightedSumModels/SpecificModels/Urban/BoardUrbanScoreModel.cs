using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Urban
{
    internal class BoardUrbanScoreModel : OverallUrbanScoreModel
    {
        public override KeyValuePair<ScoreType, double> CalculateScore(Company company)
        {
            var roles = company.Roles.ToList();

            int totalUrban = 0;
            int boardUrban = 0;
            int boardRural = 0;
            int bothUrban = 0;
            int bothRural = 0;
            int count = 0;

            for (int i = 0; i < roles.Count; i++)
            {
                Person person;
                if ((roles[i].RoleType == "Board" || roles[i].RoleType == "Both")
                    && (person = roles[i].Person) != null)
                {
                    count++;

                    if (person.Urban.HasValue)
                    {
                        totalUrban++;

                        if (roles[i].RoleType == "Board")
                        {
                            if (person.Urban.Value) boardUrban++;
                            else boardRural++;
                        }
                        else
                        {
                            if (person.Urban.Value) bothUrban++;
                            else bothRural++;
                        }
                    }

                }
            }

            if (count == 0) return new KeyValuePair<ScoreType, double>(ScoreType, 0);

            return new KeyValuePair<ScoreType, double>(ScoreType, (totalUrban > 0 ? 50d : 0) +
                (totalUrban == 0 ?
                    0d :
                    (boardUrban + bothUrban > 1) || (boardRural + bothRural > 1) ?
                        0d :
                        25d));
        }
    }
}
