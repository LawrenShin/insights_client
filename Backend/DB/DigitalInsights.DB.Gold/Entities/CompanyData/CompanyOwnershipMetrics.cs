using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
{
    public partial class CompanyOwnershipMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public bool? MinorityOwnedMajority { get; set; }
        public bool? MinorityOwned25Percents { get; set; }
        public bool? WomanOwnedMajority { get; set; }
        public bool? WomanOwned25Percents { get; set; }
        public bool? DisabledOwnedMajority { get; set; }
        public bool? DisabledOwned25Percents { get; set; }
        public bool? LGBTOwnedMajority { get; set; }
        public bool? LGBTOwned25Percents { get; set; }
        public bool? VetranOwnedMajority { get; set; }
        public bool? VeteranOwned25Percents { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
