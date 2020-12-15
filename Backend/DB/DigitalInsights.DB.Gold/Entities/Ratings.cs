using System;
using System.Collections.Generic;

namespace DigitalInsights.DB.Gold.Entities
{
    public partial class Ratings
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int RatingType { get; set; }
        public double RatingValue { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
