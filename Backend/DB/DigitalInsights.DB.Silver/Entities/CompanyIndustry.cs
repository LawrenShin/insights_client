using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class CompanyIndustry
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? IndustryId { get; set; }
        public char PrimarySecondary { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
        public virtual Industry Industry { get; set; }
    }
}
