using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities
{
    public partial class CompanyMatch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
