using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities
{
    public partial class CompanyExtendedData
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public double? BelowNationalAvgIncome { get; set; }
        public double? DisabledEmployees { get; set; }
        public int? HierarchyLevel { get; set; }
        public double? MedianSalary { get; set; }
        public double? RetentionRate { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
