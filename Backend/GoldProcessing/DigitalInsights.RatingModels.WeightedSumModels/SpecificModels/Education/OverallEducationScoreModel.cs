using DigitalInsights.DB.Gold.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.RatingModels.WeightedSumModels.SpecificModels.Education
{
    internal class OverallEducationScoreModel : ASpecificModel
    {
        public override ScoreType ScoreType => ScoreType.EducationScore;

        protected virtual bool IsRoleMatching(Role role)
        {
            return true;
        }

        public override KeyValuePair<ScoreType, double> CalculateScore(Company company)
        {
            const double HIGH_WEIGHT = 0.4d;
            const double SUBJECT_WEIGHT = 0.3d;
            const double INSTITUTION_WEIGHT = 0.3d;

            var roles = company.Roles.ToList();

            int totalHigh = 0;
            int totalSubject = 0;
            int totalInstitution = 0;
            int totalBelowUniversity = 0;
            int totalBachelor = 0;
            int totalMaster = 0;
            int totalMasterPlus = 0;
            int count = 0;


            for (int i = 0; i < roles.Count; i++)
            {
                Person person;
                if (IsRoleMatching(roles[i]) && (person = roles[i].Person) != null)
                {
                    count++;
                    if (!string.IsNullOrEmpty(person.EduSubject)) totalSubject++;
                    if (!string.IsNullOrEmpty(person.HighEdu))
                    {
                        totalHigh++;
                        if (person.HighEdu == "Elementary" || person.HighEdu == "High School") totalBelowUniversity++;
                        if (person.HighEdu == "Bachelor") totalBachelor++;
                        if (person.HighEdu == "Master") totalMaster++;
                        if (person.HighEdu == "Master+") totalMasterPlus++;
                    }
                    if (!string.IsNullOrEmpty(person.EduInstitute)) totalInstitution++;
                }
            }

            if (count == 0) return new KeyValuePair<ScoreType, double>(ScoreType,0);

            return new KeyValuePair<ScoreType, double>(ScoreType,
                HIGH_WEIGHT * (
                    (totalHigh > 0 ? 50d : 0) + 
                    (totalBachelor > 1 || totalMaster > 1 || totalMasterPlus > 1 ? 0: 25d) +
                    (totalBelowUniversity > 0 ? 25d : 0)
                ) +
                SUBJECT_WEIGHT * (totalSubject > 0 ? 50d : 0) +
                INSTITUTION_WEIGHT * (totalInstitution > 0 ? 50d : 0));
        }
    }
}