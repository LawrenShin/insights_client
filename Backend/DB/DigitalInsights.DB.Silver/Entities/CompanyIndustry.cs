using DigitalInsights.DB.Common.Enums;
using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class CompanyIndustry
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public Common.Enums.Industry Industry { get; set; }
        public IndustryCode? IndustryCode { get; set; }
        public char PrimarySecondary { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
