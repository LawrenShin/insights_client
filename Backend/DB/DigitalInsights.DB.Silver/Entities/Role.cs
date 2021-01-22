using DigitalInsights.DB.Common.Enums;
using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class Role
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int PersonId { get; set; }
        public RoleType RoleType { get; set; }
        public string Title { get; set; }
        public double? BaseSalary { get; set; }
        public double? OtherIncentives { get; set; }
        public int? JobTenure { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
        public virtual Person Person { get; set; }
    }
}
