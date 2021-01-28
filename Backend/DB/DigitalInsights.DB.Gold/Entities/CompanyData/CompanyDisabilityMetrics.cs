using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
{
    public partial class CompanyDisabilityMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? DisabelTotal { get; set; }
        public int? DisabelMental { get; set; }
        public int? DisabelPhysical { get; set; }
        public bool? DisabelProgram { get; set; }
        public bool? WheelchairAccess { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
