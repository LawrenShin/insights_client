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
            return;
            var json = @"{""id"":269304,""lei"":""254900YPEDT61W86A905"",""legalName"":""บริษัท ปตท. จำหน่ายก๊าซธรรมชาติ จำกัด"",""keyFinancialsMetrics"":{""employees"":null},""countries"":[],""industries"":[],""executiveStatistics"":{""femaleRatio"":null},""boardStatistics"":{""femaleRatio"":null},""diMetrics"":{""socialProgram"":true,""retaliation"":true,""supplySpend"":null,""valueDISupplySpend"":null,""diSupplySpendRevenueRatio"":null,""mentorProgram"":null,""socialEvents"":null,""employEngagement"":null,""employSatisfactionSurvey"":null,""employSurveyResponseRate"":22,""diPolicyEstablished"":null,""diPublicAvailable"":true,""diWebsite"":true,""diPosition"":true,""diFTEPosition"":true,""diPositionExecutive"":true},""jobMetrics"":{""totalHours"":null,""employTurnoverTotal"":99,""employTurnoverVoluntary"":22,""employTurnoverFired"":21,""employTraining"":true},""raceMetrics"":{""raceRatioExececutive"":1,""raceRatioBoard"":12,""raceRatioSenior"":44,""raceRatioMiddle"":44,""raceRatioAll"":100},""genderMetrics"":{""genderRatioSenior"":22,""genderRatioMiddle"":21,""genderRatioAll"":40,""genderPayGap"":44,""genderRatioBoard"":40},""healthMetrics"":{""fatalities"":42,""sickAbsence"":44,""healthTRI"":38,""healthTRIR"":31},""names"":[],""addresses"":[]}";

            new SilverDashboardAPI().SaveCompany(new Amazon.Lambda.APIGatewayEvents.APIGatewayProxyRequest()
            {
                Headers = new Dictionary<string, string>()
                {
                    {"Content-Type", "application/json" },
                    {"x-api-token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJ0ZXN0VXNlciIsIm5iZiI6MTYxMjUwNjEzNywiZXhwIjoxNjEyNTkyNTM3LCJpYXQiOjE2MTI1MDYxMzd9.LNm9239bzsYv7pCgbvXm2J0dNMiVGKKl4cLI8Y4CUuk" }
                },
                Body = json,
            }, null);
        }
    }
}
