using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CompanyData
{
    public partial class CompanyIdentity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public long? ISIN { get; set; }
        public long? TaxId { get; set; }
        public long? OtherNumber { get; set; }
        public string OtherLabel { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
