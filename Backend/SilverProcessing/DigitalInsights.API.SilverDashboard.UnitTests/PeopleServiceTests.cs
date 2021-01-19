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
    class PeopleServiceTests
    {
        private PeopleService peopleService;
        private SilverContext silverContext;

        [SetUp]
        public void Setup()
        {
            silverContext = new SilverContext();
            peopleService = new PeopleService(silverContext);
        }

        [Test]
        public void TestPagination()
        {
            var bigPage = peopleService.GetPeople(10, 1, null);
            Assert.NotNull(bigPage);
            Assert.NotNull(bigPage.People);
            Assert.AreEqual(10, bigPage.People.Length);

            var smallPageOne = peopleService.GetPeople(5, 2, null);
            Assert.NotNull(smallPageOne);
            Assert.NotNull(smallPageOne.People);
            Assert.AreEqual(5, smallPageOne.People.Length);

            var smallPageTwo = peopleService.GetPeople(5, 3, null);
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
            var people = peopleService.GetPeople(10, 0, prefix);

            Assert.NotZero(people.People.Length);
            foreach (var person in people.People)
            {
                Assert.IsTrue(person.Name.StartsWith(prefix));
            }
        }
    }
}
