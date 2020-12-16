using DigitalInsights.DB.Silver.Entities.CountryData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class Country
    {
        public Country()
        {
            Addresses = new HashSet<Address>();
            CompanyCountries = new HashSet<CompanyCountry>();
            CountryAges = new HashSet<CountryAge>();
            CountryDemographics = new HashSet<CountryDemographic>();
            CountryDisabilities = new HashSet<CountryDisability>();
            CountryEconomies = new HashSet<CountryEconomy>();
            CountryEdus = new HashSet<CountryEdu>();
            CountryGenders = new HashSet<CountryGender>();
            CountryPoliticals = new HashSet<CountryPolitical>();
            CountryRaces = new HashSet<CountryRace>();
            CountryReligions = new HashSet<CountryReligion>();
            CountrySexes = new HashSet<CountrySex>();
            CountryUrbans = new HashSet<CountryUrban>();
            IndustryCountries = new HashSet<IndustryCountry>();
            PersonCountries = new HashSet<PersonCountry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        [JsonIgnore]
        public DateTime EffectiveFrom { get; set; }

        [JsonIgnore]
        public virtual ICollection<Address> Addresses { get; set; }
        [JsonIgnore]
        public virtual ICollection<CompanyCountry> CompanyCountries { get; set; }
        [JsonIgnore]
        public virtual ICollection<CountryAge> CountryAges { get; set; }
        [JsonIgnore]
        public virtual ICollection<CountryDemographic> CountryDemographics { get; set; }
        [JsonIgnore]
        public virtual ICollection<CountryDisability> CountryDisabilities { get; set; }
        [JsonIgnore]
        public virtual ICollection<CountryEconomy> CountryEconomies { get; set; }
        [JsonIgnore]
        public virtual ICollection<CountryEdu> CountryEdus { get; set; }
        [JsonIgnore]
        public virtual ICollection<CountryGender> CountryGenders { get; set; }
        [JsonIgnore]
        public virtual ICollection<CountryPolitical> CountryPoliticals { get; set; }
        [JsonIgnore]
        public virtual ICollection<CountryRace> CountryRaces { get; set; }
        [JsonIgnore]
        public virtual ICollection<CountryReligion> CountryReligions { get; set; }
        [JsonIgnore]
        public virtual ICollection<CountrySex> CountrySexes { get; set; }
        [JsonIgnore]
        public virtual ICollection<CountryUrban> CountryUrbans { get; set; }
        [JsonIgnore]
        public virtual ICollection<IndustryCountry> IndustryCountries { get; set; }
        [JsonIgnore]
        public virtual ICollection<PersonCountry> PersonCountries { get; set; }
    }
}
