using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
{
    public partial class CompanyPoliticalMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public bool? NonDiscriminationPolitical { get; set; }
        public bool? SupportPolitical { get; set; }
        public bool? PoliticalVote { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
