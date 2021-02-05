using DigitalInsights.API.SilverDashboard.Services;
using DigitalInsights.Common.Logging;
using DigitalInsights.DB.Common.Enums;
using DigitalInsights.DB.Silver;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.UnitTests
{
    class PeopleServiceTests
    {
        private PeopleService peopleService;
        private SilverContext silverContext;

        [SetUp]
        public void Setup()
        {
            var factory = LoggerFactory.Create(builder =>
            {
                builder.AddProvider(new TraceLoggerProvider());
            });
            Logger.Init("PeopleService tests");
            silverContext = new SilverContext(factory);
            peopleService = new PeopleService(silverContext);
        }

        [Test]
        public void TestPagination()
        {
            var bigPage = peopleService.GetPeople(10, 1, null, null);
            Assert.NotNull(bigPage);
            Assert.NotNull(bigPage.People);
            Assert.AreEqual(10, bigPage.People.Length);

            var smallPageOne = peopleService.GetPeople(5, 2, null, null);
            Assert.NotNull(smallPageOne);
            Assert.NotNull(smallPageOne.People);
            Assert.AreEqual(5, smallPageOne.People.Length);

            var smallPageTwo = peopleService.GetPeople(5, 3, null, null);
            Assert.NotNull(smallPageTwo);
            Assert.NotNull(smallPageTwo.People);
            Assert.AreEqual(5, smallPageTwo.People.Length);

            for (int i = 0; i < 5; i++)
            {
                Assert.IsFalse(string.IsNullOrEmpty(smallPageOne.People[i].Name));
                Assert.AreEqual(smallPageOne.People[i].Name, bigPage.People[i].Name);
            }

            for (int i = 0; i < 5; i++)
            {
                Assert.IsFalse(string.IsNullOrEmpty(smallPageTwo.People[i].Name));
                Assert.AreEqual(smallPageTwo.People[i].Name, bigPage.People[i + 5].Name);
            }
        }

        [Test]
        public void SearchPrefixTest()
        {
            var prefix = "Adele";
            var people = peopleService.GetPeople(10, 0, prefix, null);

            Assert.NotZero(people.People.Length);
            foreach (var person in people.People)
            {
                Assert.IsTrue(person.Name.StartsWith(prefix));
            }
        }

        [Test]
        public void SearchCompanyIdTest()
        {
            var companyId = 1246216;
            var people = peopleService.GetPeople(20, 0, null, companyId);

            Assert.NotZero(people.People.Length);
            foreach (var person in people.People)
            {
                Assert.IsTrue(person.Roles.Any(x=>x.CompanyId == companyId));
            }
        }
    }
}
