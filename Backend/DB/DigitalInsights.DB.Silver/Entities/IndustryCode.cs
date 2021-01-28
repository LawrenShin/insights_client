using DigitalInsights.DB.Silver.Entities.CompanyData;
using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class IndustryCode
    {
        public IndustryCode()
        {
            CompanyIndustries = new HashSet<CompanyIndustry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompanyIndustry> CompanyIndustries { get; set; }
    }
}
