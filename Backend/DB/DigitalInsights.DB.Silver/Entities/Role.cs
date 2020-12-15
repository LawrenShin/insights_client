using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class Role
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? PersonId { get; set; }
        public short? IsEffective { get; set; }
        public string RoleType { get; set; }
        public string Title { get; set; }
        public int? BaseSalary { get; set; }
        public string IncentiveOptions { get; set; }
        public DateTime EffectiveFrom { get; set; }

        [JsonIgnore]
        public virtual Company Company { get; set; }
        public virtual Person Person { get; set; }
    }
}
