using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace MetaQuotes.Services
{
    public class GeoRepository : IGeoRepository
    {
        private Header header;
        private List<Location> locations;
        private List<IPRange> ranges;
        private List<int> citySortPosition;

        public GeoRepository()
        {
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
        }

        public Location findLocationByIp(string ip)
        {
            long ipNumerical = convertIp(ip);
            var range = findRangeByIp(ipNumerical);
            if (range == null)
                return null;
            return locations[(int)range.location_index];
        }

        private IPRange findRangeByIp(long ip)
        {
            int from = 0;
            int to = ranges.Count - 1;
            while (to > from)
            {
                int middle = (to + from) / 2;
                var middleElement = ranges[middle];
                if (middleElement.ip_from > ip)
                {
                    to = middle - 1;
                }
                else if (middleElement.ip_to < ip)
                {
                    from = middle + 1;
                }
                else
                {
                    return middleElement;
                }
            }

            var range = ranges[from];
            if (range.ip_from <= ip && range.ip_to >= ip)
                return range;

            return null;
        }

        public List<Location> findLocationsByCityName(string city)
        {
            Thread.Sleep(5000);
            int from = 0;
            int to = citySortPosition.Count - 1;
            while (to > from)
            {
                int middle = (to + from) / 2;
                var middleElement = locations[citySortPosition[middle]];
                var compare = middleElement.city.CompareTo(city);
                if (compare > 0)
                {
                    to = middle - 1;
                }
                else if (compare < 0)
                {
                    from = middle + 1;
                }
                else
                {
                    return getLocationsWithSameCityName(middle);
                }
            }

            var location = locations[citySortPosition[from]];
            if (location.city == city)
                return getLocationsWithSameCityName(from);

            return new List<Location>();
        }

        private List<Location> getLocationsWithSameCityName(int position)
        {
            List<Location> result = new List<Location>();
            var location = locations[citySortPosition[position]];

            result.Add(location);

            if (position > 0)
            {
                int currentPost = position;
                Location locationToCheck;
                while ((locationToCheck = locations[citySortPosition[--currentPost]]).city == location.city)
                {
                    result.Add(locationToCheck);
                }
            }

            if (position < citySortPosition.Count-2)
            {
                int currentPost = position;
                Location locationToCheck;
                while ((locationToCheck = locations[citySortPosition[++currentPost]]).city == location.city)
                {
                    result.Add(locationToCheck);
                }
            }

            return result;
        }

        private long convertIp(string ip)
        {
            string[] addrArray = ip.Split(".");

            if (addrArray.Length != 4)
                throw new ArgumentException();

            long num = 0;
            for (int i = 0; i < 4; i++)
            {
                int part = int.Parse(addrArray[i]);
                if (part < 0 || part > 256)
                    throw new ArgumentException();

                num += part * Math.Max(256 * (3 - i), 1);
            }

            return num;
        }

    }
}
