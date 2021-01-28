using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
{
    public partial class CompanyName
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public string NameType { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
