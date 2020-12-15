using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities
{
    public partial class CompanyOperation
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? CountryId { get; set; }
        public string Ticker { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
        public virtual Country Country { get; set; }
    }
}
