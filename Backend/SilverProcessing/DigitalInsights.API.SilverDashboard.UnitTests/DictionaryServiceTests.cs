using DigitalInsights.API.SilverDashboard.Services;
using DigitalInsights.DB.Common.Enums;
using DigitalInsights.DB.Silver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.UnitTests
{
    class DictionaryServiceTests
    {
        private DictionaryService dictionaryService;
        private SilverContext silverContext;

        [SetUp]
        public void Setup()
        {
            silverContext = new SilverContext();
            dictionaryService = new DictionaryService(silverContext);
        }

        [Test]
        public void TestDictionaries()
        {
            var countries = dictionaryService.GetCountries();
            Assert.NotNull(countries);
            Assert.IsTrue(countries.Length > 0);
            foreach(var country in countries)
            {
                Assert.IsTrue(country.Id > 0);
                Assert.IsFalse(string.IsNullOrEmpty(country.ISOCode));
                Assert.IsFalse(string.IsNullOrEmpty(country.Name));
            }

            var educationLevels = dictionaryService.GetEducationLevels();
            Assert.NotNull(educationLevels);
            Assert.AreEqual(Enum.GetNames(typeof(EducationLevel)).Length, educationLevels.Length);
            var enumEducationLevels = Enum.GetValues<EducationLevel>().Select(x => (int)x).ToHashSet();
            foreach (var educationLevel in educationLevels)
            {
                Assert.IsTrue(enumEducationLevels.Contains(educationLevel.Id));
                enumEducationLevels.Remove(educationLevel.Id);
                Assert.IsFalse(string.IsNullOrEmpty(educationLevel.Name));
            }

            var educationSubjects = dictionaryService.GetEducationSubjects();
            Assert.NotNull(educationSubjects);
            Assert.AreEqual(Enum.GetNames(typeof(EducationSubject)).Length, educationSubjects.Length);
            var enumEducationSubjects = Enum.GetValues<EducationSubject>().Select(x => (int)x).ToHashSet();
            foreach (var educationSubject in educationSubjects)
            {
                Assert.IsTrue(enumEducationSubjects.Contains(educationSubject.Id));
                enumEducationSubjects.Remove(educationSubject.Id);
                Assert.IsFalse(string.IsNullOrEmpty(educationSubject.Name));
            }

            var genders = dictionaryService.GetGenders();
            Assert.NotNull(genders);
            Assert.AreEqual(Enum.GetNames(typeof(Gender)).Length, genders.Length);
            var enumGenders = Enum.GetValues<Gender>().Select(x => (int)x).ToHashSet();
            foreach (var gender in genders)
            {
                Assert.IsTrue(enumGenders.Contains(gender.Id));
                enumGenders.Remove(gender.Id);
                Assert.IsFalse(string.IsNullOrEmpty(gender.Name));
            }

            var industries = dictionaryService.GetIndustries();
            Assert.NotNull(industries);
            Assert.AreEqual(Enum.GetNames(typeof(Industry)).Length, industries.Length);
            var enumIndustries = Enum.GetValues<Industry>().Select(x => (int)x).ToHashSet();
            foreach (var industry in industries)
            {
                Assert.IsTrue(enumIndustries.Contains(industry.Id));
                enumIndustries.Remove(industry.Id);
                Assert.IsFalse(string.IsNullOrEmpty(industry.Name));
            }

            var industryCodes = dictionaryService.GetIndustryCodes();
            Assert.NotNull(industryCodes);
            Assert.AreEqual(Enum.GetNames(typeof(IndustryCode)).Length, industryCodes.Length);
            var enumIndustryCodes = Enum.GetValues<IndustryCode>().Select(x => (int)x).ToHashSet();
            foreach (var industryCode in industryCodes)
            {
                Assert.IsTrue(enumIndustryCodes.Contains(industryCode.Id));
                enumIndustryCodes.Remove(industryCode.Id);
                Assert.IsFalse(string.IsNullOrEmpty(industryCode.Name));
            }

            var maritalStatuses = dictionaryService.GetMaritalStatuses();
            Assert.NotNull(maritalStatuses);
            Assert.AreEqual(Enum.GetNames(typeof(MaritalStatus)).Length, maritalStatuses.Length);
            var enumMaritalStatuses = Enum.GetValues<MaritalStatus>().Select(x => (int)x).ToHashSet();
            foreach (var maritalStatus in maritalStatuses)
            {
                Assert.IsTrue(enumMaritalStatuses.Contains(maritalStatus.Id));
                enumMaritalStatuses.Remove(maritalStatus.Id);
                Assert.IsFalse(string.IsNullOrEmpty(maritalStatus.Name));
            }

            var races = dictionaryService.GetRaces();
            Assert.NotNull(races);
            Assert.AreEqual(Enum.GetNames(typeof(Race)).Length, races.Length);
            var enumRaces = Enum.GetValues<Race>().Select(x => (int)x).ToHashSet();
            foreach (var race in races)
            {
                Assert.IsTrue(enumRaces.Contains(race.Id));
                enumRaces.Remove(race.Id);
                Assert.IsFalse(string.IsNullOrEmpty(race.Name));
            }

            var regions = dictionaryService.GetRegions();
            Assert.NotNull(regions);
            Assert.AreEqual(Enum.GetNames(typeof(Region)).Length, regions.Length);
            var enumRegions = Enum.GetValues<Region>().Select(x => (int)x).ToHashSet();
            foreach (var region in regions)
            {
                Assert.IsTrue(enumRegions.Contains(region.Id));
                enumRegions.Remove(region.Id);
                Assert.IsFalse(string.IsNullOrEmpty(region.Name));
            }

            var religions = dictionaryService.GetReligions();
            Assert.NotNull(religions);
            Assert.AreEqual(Enum.GetNames(typeof(Religion)).Length, religions.Length);
            var enumReligions = Enum.GetValues<Religion>().Select(x => (int)x).ToHashSet();
            foreach (var religion in religions)
            {
                Assert.IsTrue(enumReligions.Contains(religion.Id));
                enumReligions.Remove(religion.Id);
                Assert.IsFalse(string.IsNullOrEmpty(religion.Name));
            }

            var roleTypes = dictionaryService.GetRoleTypes();
            Assert.NotNull(roleTypes);
            Assert.AreEqual(Enum.GetNames(typeof(RoleType)).Length, roleTypes.Length);
            var enumRoleTypes = Enum.GetValues<RoleType>().Select(x => (int)x).ToHashSet();
            foreach (var roleType in roleTypes)
            {
                Assert.IsTrue(enumRoleTypes.Contains(roleType.Id));
                enumRoleTypes.Remove(roleType.Id);
                Assert.IsFalse(string.IsNullOrEmpty(roleType.Name));
            }
        }
    }
}
