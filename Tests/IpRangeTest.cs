using MetaQuotes.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class IpRangeTest
    {
        IGeoRepository geoRepository;
        IGeoService getService;

        [SetUp]
        public void Setup()
        {
            var mock = new Mock<IGeoRepository>();
            mock.Setup(a => a.getLocations()).Returns(new List<Location>() {
                createLocation("Test1"),
                createLocation("Test2"),
                createLocation("Test3"),
                createLocation("Test4"),
            });
            mock.Setup(a => a.getRanges()).Returns(new List<IPRange>() {
                createRange(0,10,0),
                createRange(11,20,2),
                createRange(21,30,1),
                createRange(31,120,0),
                createRange(121,300,2),
                createRange(1*256*256, 1*256*256 + 256, 3),
            });
            getService = new GeoService(mock.Object);
        }

        private Location createLocation(string city)
        {
            return new Location
            {
                city = city,
            };
        }

        private IPRange createRange(uint ip_from, uint ip_to, uint location_index)
        {
            return new IPRange
            {
                ip_from = ip_from,
                ip_to = ip_to,
                location_index = location_index,
            };
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(getService.findLocationByIp("0.0.0.15").city, "Test3");
            Assert.AreEqual(getService.findLocationByIp("0.0.0.0").city, "Test1");
            Assert.AreEqual(getService.findLocationByIp("0.1.0.30").city, "Test4");
            Assert.IsNull(getService.findLocationByIp("1.1.0.30"));
        }
    }
}