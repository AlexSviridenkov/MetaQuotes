using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;

namespace MetaQuotes.Services
{
    public class GeoRepository : IGeoRepository
    {

        private readonly ILogger _logger;

        private Header header;
        private List<Location> locations;
        private List<IPRange> ranges;
        private List<int> citySortPosition;

        public GeoRepository(ILogger<GeoRepository> logger)
        {
            _logger = logger;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            using (FileStream fsSource = new FileStream(@"data\geobase.dat", FileMode.Open, FileAccess.Read))
            {
                byte[] buf = new byte[fsSource.Length];
                fsSource.Read(buf, 0, (int)fsSource.Length);

                var parser = new DatabaseParser();
                header = parser.parseHeader(buf);
                locations = parser.parseLocations(header, buf);
                ranges = parser.parseRanges(header, buf);
                citySortPosition = parser.parseCitySortPosition(header, buf);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            _logger.LogDebug($"Data loading took {elapsedMs} ms");
        }

        public List<int> getCitySortPosition()
        {
            return citySortPosition;
        }

        public List<Location> getLocations()
        {
            return locations;
        }

        public List<IPRange> getRanges()
        {
            return ranges;
        }
    }
}
