using DigitalInsights.API.SilverDashboard.Services;
using DigitalInsights.DB.Silver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.UnitTests
{
    class CompanyServiceTests
    {
        private CompanyService companyService;
        private SilverContext silverContext;

        [SetUp]
        public void Setup()
        {
            silverContext = new SilverContext();
            companyService = new CompanyService(silverContext);
        }

        [Test]
        public void SanityTest()
        {
            var companies = companyService.GetCompanies(10, 0, null);

            Assert.NotNull(companies);
            Assert.NotNull(companies.Companies);
            Assert.AreEqual(10, companies.Companies.Length);

            Assert.NotNull(companies.Pagination);
            Assert.AreEqual(10, companies.Pagination.PageSize);
            Assert.AreEqual(0, companies.Pagination.PageIndex);
            Assert.IsTrue(companies.Pagination.PageCount > 0);
        }

        [Test]
        public void CompanyCountriesISOCodeTest()
        {
            var companies = companyService.GetCompanies(10, 0, null);
            foreach(var company in companies.Companies)
            {
                Assert.NotNull(company.CompanyCountries);
                Assert.NotZero(company.CompanyCountries.Length);
                foreach(var companyCountry in company.CompanyCountries)
                {
                    Assert.IsFalse(string.IsNullOrEmpty(companyCountry.ISOCode));
                }
            }
        }
    }
}
