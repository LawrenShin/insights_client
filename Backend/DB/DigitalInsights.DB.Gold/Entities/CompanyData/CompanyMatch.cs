using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
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
