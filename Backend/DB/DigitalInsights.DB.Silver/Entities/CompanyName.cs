using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class CompanyName
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        [JsonIgnore]
        public virtual Company Company { get; set; }
    }
}
