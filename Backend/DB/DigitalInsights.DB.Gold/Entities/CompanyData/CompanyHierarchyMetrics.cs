using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
{
    public partial class CompanyHierarchyMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? HierachyLevel { get; set; }
        public bool? TownHalls { get; set; }
        public bool? Intranet { get; set; }
        public bool? OrganizationalStructure { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
