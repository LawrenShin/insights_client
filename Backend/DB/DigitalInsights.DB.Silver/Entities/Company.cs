using Newtonsoft.Json;
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
            CompanyExtendedData = new HashSet<CompanyExtendedData>();
            CompanyIndustries = new HashSet<CompanyIndustry>();
            CompanyMatches = new HashSet<CompanyMatch>();
            CompanyNames = new HashSet<CompanyName>();
            CompanyQuestionnaires = new HashSet<CompanyQuestion>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Lei { get; set; }
        public string LegalName { get; set; }
        public string Status { get; set; }
        public int? NumEmployees { get; set; }
        [JsonIgnore]
        public int? LegalId { get; set; }
        [JsonIgnore]
        public int? HqId { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Address Hq { get; set; }
        public virtual Address Legal { get; set; }
        public virtual ICollection<CompanyCountry> CompanyCountries { get; set; }
        public virtual ICollection<CompanyExtendedData> CompanyExtendedData { get; set; }
        public virtual ICollection<CompanyIndustry> CompanyIndustries { get; set; }
        [JsonIgnore]
        public virtual ICollection<CompanyMatch> CompanyMatches { get; set; }
        public virtual ICollection<CompanyName> CompanyNames { get; set; }
        public virtual ICollection<CompanyQuestion> CompanyQuestionnaires { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
