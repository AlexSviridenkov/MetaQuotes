using MetaQuotes.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class CitiesTest
    {
        IGeoRepository geoRepository;
        IGeoService getService;

        [SetUp]
        public void Setup()
        {
            var mock = new Mock<IGeoRepository>();
            mock.Setup(a => a.getLocations()).Returns(new List<Location>() {
                createLocation("Test1", "PT1"),
                createLocation("Test2", ""),
                createLocation("Test5", ""),
                createLocation("Test3", "PT3"),
                createLocation("Test3", "PT2"),
                createLocation("Test6", ""),
                createLocation("Test7", ""),
            });

            mock.Setup(a => a.getCitySortPosition()).Returns(new List<int>() {
                0,
                1,
                3,
                4,
                2,
                5,
                6
            });
            getService = new GeoService(mock.Object);
        }

        private Location createLocation(string city, string postal)
        {
            return new Location
            {
                city = city,
                postal = postal
            };
        }

        [Test]
        public void TestSimple()
        {
            var locations = getService.findLocationsByCityName("Test1");
            Assert.AreEqual(locations.Count, 1);
            Assert.AreEqual(locations[0].postal, "PT1");
        }

        [Test]
        public void TestComplex()
        {
            var locations = getService.findLocationsByCityName("Test3");
            Assert.AreEqual(locations.Count, 2);
            Assert.AreEqual(locations[0].postal, "PT3");
            Assert.AreEqual(locations[1].postal, "PT2");
        }
    }
}