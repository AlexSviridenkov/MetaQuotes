using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaQuotes.Services
{
    public interface IGeoRepository
    {
        List<Location> getLocations();
        List<IPRange> getRanges();
        List<int> getCitySortPosition();
    }
}
