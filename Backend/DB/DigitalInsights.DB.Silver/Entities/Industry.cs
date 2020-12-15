using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class Industry
    {
        public Industry()
        {
            CompanyIndustries = new HashSet<CompanyIndustry>();
            IndustryCountries = new HashSet<IndustryCountry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual ICollection<CompanyIndustry> CompanyIndustries { get; set; }
        public virtual ICollection<IndustryCountry> IndustryCountries { get; set; }
    }
}
