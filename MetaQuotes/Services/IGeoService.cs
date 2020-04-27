using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaQuotes.Services
{
    public interface IGeoService
    {
        List<Location> findLocationsByCityName(string city);
        Location findLocationByIp(string ip);
    }
}
