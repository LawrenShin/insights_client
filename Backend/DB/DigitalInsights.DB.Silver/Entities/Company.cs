using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class Company
    {
        public Company()
        {
            CompanyCountries = new HashSet<CompanyCountry>();
            CompanyIndustries = new HashSet<CompanyIndustry>();
            CompanyMatches = new HashSet<CompanyMatch>();
            CompanyNames = new HashSet<CompanyName>();
            CompanyPrivateData = new HashSet<CompanyPrivateData>();
            CompanyPublicData = new HashSet<CompanyPublicData>();
            CompanyQuestionnaires = new HashSet<CompanyQuestion>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Lei { get; set; }
        public string LegalName { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual ICollection<CompanyCountry> CompanyCountries { get; set; }
        public virtual ICollection<CompanyIndustry> CompanyIndustries { get; set; }
        public virtual ICollection<CompanyMatch> CompanyMatches { get; set; }
        public virtual ICollection<CompanyName> CompanyNames { get; set; }
        public virtual ICollection<CompanyPrivateData> CompanyPrivateData { get; set; }
        public virtual ICollection<CompanyPublicData> CompanyPublicData { get; set; }
        public virtual ICollection<CompanyQuestion> CompanyQuestionnaires { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
