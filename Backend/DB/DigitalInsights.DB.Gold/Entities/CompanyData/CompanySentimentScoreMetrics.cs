using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
{
    public partial class CompanySentimentScoreMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? SentimentNegative { get; set; }
        public int? SentimentPositive { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
