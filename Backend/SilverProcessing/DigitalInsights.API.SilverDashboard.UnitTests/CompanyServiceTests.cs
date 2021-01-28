using DigitalInsights.API.SilverDashboard.Services;
using DigitalInsights.Common.Logging;
using DigitalInsights.DB.Silver;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            var factory = LoggerFactory.Create(builder =>
            {
                builder.AddProvider(new TraceLoggerProvider());
            });
            Logger.Init("CompanyServiceTests");
            silverContext = new SilverContext(factory);
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
                Assert.NotNull(company.Countries);
                Assert.NotZero(company.Countries.Length);
                foreach(var companyCountry in company.Countries)
                {
                    Assert.IsFalse(string.IsNullOrEmpty(companyCountry.IsoCode));
                }
            }
        }

        [Test]
        public void SearchPrefixTest()
        {
            var prefix = "APPLE";
            var companies = companyService.GetCompanies(10, 0, prefix);

            Assert.NotZero(companies.Companies.Length);
            foreach (var company in companies.Companies)
            {
                Assert.IsTrue(company.LegalName.StartsWith(prefix));
            }
        }

        [Test]
        public void MetadataSanityTest()
        {
            Assert.DoesNotThrow(() =>
            {
                var result = companyService.GetCompanies(10, 0, null);
                var serializationResult = JsonConvert.SerializeObject(
                            result,
                            new JsonSerializerSettings() { ContractResolver = new MetadataBasedContractResolver() });
            });
        }

        [Test]
        public void TemporaryIntegrationTest()
        {
            var json = @"{legalName: ""lets test it out"",
boardStatistics: {},
diMetrics: {diPolicyEstablished: true, diPublicAvailable: true},
diPolicyEstablished: true,
diPublicAvailable: true,
executiveStatistics: {},
genderMetrics: {genderRatioBoard: 44,
genderRatioBoard: 44},
healthMetrics: {fatalities: 53531, healthTRI: 3,
fatalities: 53531,
healthTRI: 3},
jobMetrics: {employTurnoverTotal: 38,
employTurnoverTotal: 38},
keyFinancialsMetrics: {},
raceMetrics: { }}";

            new SilverDashboardAPI().SaveCompany(new Amazon.Lambda.APIGatewayEvents.APIGatewayProxyRequest()
            {
                Headers = new Dictionary<string, string>()
                {
                    {"Content-Type", "application/json" },
                    {"x-api-token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMjMiLCJuYmYiOjE2MTEzMzE2ODIsImV4cCI6MTYxMjYyNzY4MiwiaWF0IjoxNjExMzMxNjgyLCJpc3MiOiJodHRwczovL2RpZ2l0YWwtaW5zaWdodHMuY29tIiwiYXVkIjoiaHR0cHM6Ly9kaWdpdGFsLWluc2lnaHRzLmNvbSJ9.J3-LPboKm2PIptnH-xJSAEtxSwFtpo-eN5_TPsx9byo" }
                },
                Body = json,
            }, null);
        }
    }
}
