using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CompanyData
{
    public partial class CompanyLegalInformation
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string LegalForm { get; set; }
        public bool? CompanyPublic { get; set; }
        public string CompanyIndex { get; set; }
        public string Status { get; set; }
        public DateTime? IncorporationDate { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
