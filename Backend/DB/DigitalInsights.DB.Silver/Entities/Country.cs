using DigitalInsights.DB.Silver.Entities.CountryData;
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
            CountryDemographics = new HashSet<CountryDemographics>();
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

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<CompanyCountry> CompanyCountries { get; set; }
        public virtual ICollection<CountryAge> CountryAges { get; set; }
        public virtual ICollection<CountryDemographics> CountryDemographics { get; set; }
        public virtual ICollection<CountryDisability> CountryDisabilities { get; set; }
        public virtual ICollection<CountryEconomy> CountryEconomies { get; set; }
        public virtual ICollection<CountryEdu> CountryEdus { get; set; }
        public virtual ICollection<CountryGender> CountryGenders { get; set; }
        public virtual ICollection<CountryPolitical> CountryPoliticals { get; set; }
        public virtual ICollection<CountryRace> CountryRaces { get; set; }
        public virtual ICollection<CountryReligion> CountryReligions { get; set; }
        public virtual ICollection<CountrySex> CountrySexes { get; set; }
        public virtual ICollection<CountryUrban> CountryUrbans { get; set; }
        public virtual ICollection<IndustryCountry> IndustryCountries { get; set; }
        public virtual ICollection<PersonCountry> PersonCountries { get; set; }
    }
}
