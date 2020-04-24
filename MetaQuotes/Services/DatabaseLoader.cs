using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MetaQuotes.Services
{
    public class DatabaseParser
    {
        private readonly Header header;
        private readonly List<Location> locations;
        private readonly List<IPRange> ranges;
        private readonly List<int> countrySortPosition;

        private static int threadCount = Environment.ProcessorCount;

        public List<Location> parseLocations(Header header, byte[] buf)
        {
            return new ParallelParser<Location>(threadCount, 96, parseLocation).parse(buf, (int)header.offset_locations, header.records);
        }

        public List<IPRange> parseRanges(Header header, byte[] buf)
        {
            return new ParallelParser<IPRange>(threadCount, 12, parseRange).parse(buf, (int)header.offset_ranges, header.records);
        }

        public List<int> parseCitySortPosition(Header header, byte[] buf)
        {
            return new ParallelParser<int>(threadCount, 4, (buff, pos) =>
            {
                return (int)(BitConverter.ToInt32(buff, pos)) / 96;
            }).parse(buf, (int)header.offset_cities, header.records); ;
        }


        public Header parseHeader(byte[] buf)
        {
            Header header = new Header();
            int pos = 0;
            header.version = BitConverter.ToInt32(buf, pos);
            pos += 4;
            header.name = Encoding.UTF8.GetString(buf, pos, 32).TrimEnd('\0');
            pos += 32;
            header.timestamp = BitConverter.ToUInt64(buf, pos);
            pos += 8;
            header.records = BitConverter.ToInt32(buf, pos);
            pos += 4;
            header.offset_ranges = BitConverter.ToUInt32(buf, pos);
            pos += 4;
            header.offset_cities = BitConverter.ToUInt32(buf, pos);
            pos += 4;
            header.offset_locations = BitConverter.ToUInt32(buf, pos);
            return header;
        }

        private Location parseLocation(byte[] buf, int pos)
        {
            var location = new Location();
            location.country = GetString(buf, pos, 8);
            pos += 8;
            location.region = GetString(buf, pos, 12);
            pos += 12;
            location.postal = GetString(buf, pos, 12);
            pos += 12;
            location.city = GetString(buf, pos, 24);
            pos += 24;
            location.organization = GetString(buf, pos, 32);
            pos += 32;
            location.latitude = BitConverter.ToSingle(buf, pos);
            pos += 4;
            location.longitude = BitConverter.ToSingle(buf, pos);
            pos += 4;
            return location;
        }

        private IPRange parseRange(byte[] buf, int pos)
        {
            var range = new IPRange();
            range.ip_from = BitConverter.ToUInt32(buf, pos);
            pos += 4;
            range.ip_to = BitConverter.ToUInt32(buf, pos);
            pos += 4;
            range.location_index = BitConverter.ToUInt32(buf, pos);
            pos += 4;
            return range;
        }

        string GetString(byte[] buf, int pos, int lenght)
        {
            while (lenght > 0 && buf[pos + lenght - 1] == 0)
            {
                lenght--;
            }
            return Encoding.UTF8.GetString(buf, pos, lenght);
        }
    };
}
