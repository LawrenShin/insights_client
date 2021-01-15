using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class CompanyQuestion
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public Common.Enums.CompanyQuestion Question { get; set; }
        public int Answer { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
