using DigitalInsights.DB.Silver.Entities;
using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CompanyData
{
    public partial class CompanyEducationMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public double? ElementaryShare { get; set; }
        public double? HighschoolShare { get; set; }
        public double? BachelorShare { get; set; }
        public double? MasterShare { get; set; }
        public bool? EducationLeaveSupport { get; set; }
        public bool? EducationSupportProgram { get; set; }
        public bool? StudentDebt { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
