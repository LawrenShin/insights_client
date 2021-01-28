using DigitalInsights.DB.Gold.Entities.CompanyData;
using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities
{
    public partial class Company
    {
        public Company()
        {
            CompanyAddresses = new HashSet<Address>();
            CompanyBoardStatistics = new HashSet<CompanyBoardStatistics>();
            CompanyCountries = new HashSet<CompanyCountry>();
            CompanyDIMetrics = new HashSet<CompanyDIMetrics>();
            CompanyDisabilityMetrics = new HashSet<CompanyDisabilityMetrics>();
            CompanyEducationMetrics = new HashSet<CompanyEducationMetrics>();
            CompanyExecutiveStatistics = new HashSet<CompanyExecutiveStatistics>();
            CompanyFamilyMetrics = new HashSet<CompanyFamilyMetrics>();
            CompanyGenderMetrics = new HashSet<CompanyGenderMetrics>();
            CompanyHealthMetrics = new HashSet<CompanyHealthMetrics>();
            CompanyHierarchyMetrics = new HashSet<CompanyHierarchyMetrics>();
            CompanyIdentities = new HashSet<CompanyIdentity>();
            CompanyIndustries = new HashSet<CompanyIndustry>();
            CompanyJobMetrics = new HashSet<CompanyJobMetrics>();
            CompanyKeyFinancialsMetrics = new HashSet<CompanyKeyFinancialsMetrics>();
            CompanyLegalInformations = new HashSet<CompanyLegalInformation>();
            CompanyMatches = new HashSet<CompanyMatch>();
            CompanyNames = new HashSet<CompanyName>();
            CompanyNationalityMetrics = new HashSet<CompanyNationalityMetrics>();
            CompanyOwnershipMetrics = new HashSet<CompanyOwnershipMetrics>();
            CompanyPoliticalMetrics = new HashSet<CompanyPoliticalMetrics>();
            CompanyRaceMetrics = new HashSet<CompanyRaceMetrics>();
            CompanyReligionMetrics = new HashSet<CompanyReligionMetrics>();
            CompanySentimentScoreMetrics = new HashSet<CompanySentimentScoreMetrics>();
            CompanySexualityMetrics = new HashSet<CompanySexualityMetrics>();
            CompanyUrbanizationMetrics = new HashSet<CompanyUrbanizationMetrics>();
            InverseDirectParentNavigation = new HashSet<Company>();
            InverseUltimateParentNavigation = new HashSet<Company>();
            Ratings = new HashSet<Rating>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string LEI { get; set; }
        public string LegalName { get; set; }
        public int? DirectParent { get; set; }
        public int? UltimateParent { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company DirectParentNavigation { get; set; }
        public virtual Company UltimateParentNavigation { get; set; }

        public virtual ICollection<Address> CompanyAddresses { get; set; }
        public virtual ICollection<CompanyBoardStatistics> CompanyBoardStatistics { get; set; }
        public virtual ICollection<CompanyCountry> CompanyCountries { get; set; }
        public virtual ICollection<CompanyDIMetrics> CompanyDIMetrics { get; set; }
        public virtual ICollection<CompanyDisabilityMetrics> CompanyDisabilityMetrics { get; set; }
        public virtual ICollection<CompanyEducationMetrics> CompanyEducationMetrics { get; set; }
        public virtual ICollection<CompanyExecutiveStatistics> CompanyExecutiveStatistics { get; set; }
        public virtual ICollection<CompanyFamilyMetrics> CompanyFamilyMetrics { get; set; }
        public virtual ICollection<CompanyGenderMetrics> CompanyGenderMetrics { get; set; }
        public virtual ICollection<CompanyHealthMetrics> CompanyHealthMetrics { get; set; }
        public virtual ICollection<CompanyHierarchyMetrics> CompanyHierarchyMetrics { get; set; }
        public virtual ICollection<CompanyIdentity> CompanyIdentities { get; set; }
        public virtual ICollection<CompanyIndustry> CompanyIndustries { get; set; }
        public virtual ICollection<CompanyJobMetrics> CompanyJobMetrics { get; set; }
        public virtual ICollection<CompanyKeyFinancialsMetrics> CompanyKeyFinancialsMetrics { get; set; }
        public virtual ICollection<CompanyLegalInformation> CompanyLegalInformations { get; set; }
        public virtual ICollection<CompanyMatch> CompanyMatches { get; set; }
        public virtual ICollection<CompanyName> CompanyNames { get; set; }
        public virtual ICollection<CompanyNationalityMetrics> CompanyNationalityMetrics { get; set; }
        public virtual ICollection<CompanyOwnershipMetrics> CompanyOwnershipMetrics { get; set; }
        public virtual ICollection<CompanyPoliticalMetrics> CompanyPoliticalMetrics { get; set; }
        public virtual ICollection<CompanyRaceMetrics> CompanyRaceMetrics { get; set; }
        public virtual ICollection<CompanyReligionMetrics> CompanyReligionMetrics { get; set; }
        public virtual ICollection<CompanySentimentScoreMetrics> CompanySentimentScoreMetrics { get; set; }
        public virtual ICollection<CompanySexualityMetrics> CompanySexualityMetrics { get; set; }
        public virtual ICollection<CompanyUrbanizationMetrics> CompanyUrbanizationMetrics { get; set; }
        public virtual ICollection<Company> InverseDirectParentNavigation { get; set; }
        public virtual ICollection<Company> InverseUltimateParentNavigation { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
