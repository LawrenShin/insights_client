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
    class MetadataServiceTests
    {
        private MetadataService metadataService;
        private SilverContext silverContext;

        [SetUp]
        public void Setup()
        {
            silverContext = new SilverContext();
            metadataService = new MetadataService(silverContext);
        }

        [Test]
        public void MetadataSanityTest()
        {
            var result = metadataService.GetUIMetadata("company");

            Assert.NotNull(result);
            Assert.NotZero(result.Length);
            var meta = result[0];
            Assert.AreEqual("company", meta.EntityName);
        }
    }
}
