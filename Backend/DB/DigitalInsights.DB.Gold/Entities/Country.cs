using DigitalInsights.DB.Gold.Entities.CompanyData;
using DigitalInsights.DB.Gold.Entities.CountryData;
using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities
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
            CountryEconomicEqualities = new HashSet<CountryEconomicEquality>();
            CountryEconomicPowers = new HashSet<CountryEconomicPower>();
            CountryEducations = new HashSet<CountryEducation>();
            CountryGenders = new HashSet<CountryGender>();
            CountryIndustries = new HashSet<CountryIndustry>();
            CountryInfrastructures = new HashSet<CountryInfrastructure>();
            CountryLaborAndSocialProtections = new HashSet<CountryLaborAndSocialProtection>();
            CountryLaborForces = new HashSet<CountryLaborForce>();
            CountryPoliticals = new HashSet<CountryPolitical>();
            CountryPrivateSectorsAndTrades = new HashSet<CountryPrivateSectorAndTrade>();
            CountryPublicSectors = new HashSet<CountryPublicSector>();
            CountryRaces = new HashSet<CountryRace>();
            CountryReligions = new HashSet<CountryReligion>();
            CountrySexualities = new HashSet<CountrySexuality>();
            CountryUrbanizations = new HashSet<CountryUrbanization>();
            CountryUtilities = new HashSet<CountryUtility>();
            PersonNationalities = new HashSet<PersonNationality>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ISOCode { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<CompanyCountry> CompanyCountries { get; set; }
        public virtual ICollection<CountryAge> CountryAges { get; set; }
        public virtual ICollection<CountryDemographics> CountryDemographics { get; set; }
        public virtual ICollection<CountryDisability> CountryDisabilities { get; set; }
        public virtual ICollection<CountryEconomicEquality> CountryEconomicEqualities { get; set; }
        public virtual ICollection<CountryEconomicPower> CountryEconomicPowers { get; set; }
        public virtual ICollection<CountryEducation> CountryEducations { get; set; }
        public virtual ICollection<CountryGender> CountryGenders { get; set; }
        public virtual ICollection<CountryIndustry> CountryIndustries { get; set; }
        public virtual ICollection<CountryInfrastructure> CountryInfrastructures { get; set; }
        public virtual ICollection<CountryLaborAndSocialProtection> CountryLaborAndSocialProtections { get; set; }
        public virtual ICollection<CountryLaborForce> CountryLaborForces { get; set; }
        public virtual ICollection<CountryPolitical> CountryPoliticals { get; set; }
        public virtual ICollection<CountryPrivateSectorAndTrade> CountryPrivateSectorsAndTrades { get; set; }
        public virtual ICollection<CountryPublicSector> CountryPublicSectors { get; set; }
        public virtual ICollection<CountryRace> CountryRaces { get; set; }
        public virtual ICollection<CountryReligion> CountryReligions { get; set; }
        public virtual ICollection<CountrySexuality> CountrySexualities { get; set; }
        public virtual ICollection<CountryUrbanization> CountryUrbanizations { get; set; }
        public virtual ICollection<CountryUtility> CountryUtilities { get; set; }
        public virtual ICollection<PersonNationality> PersonNationalities { get; set; }
    }
}
