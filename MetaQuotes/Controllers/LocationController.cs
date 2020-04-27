using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using MetaQuotes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetaQuotes.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class LocationController : ControllerBase
    {

        private readonly IGeoService geoService;

        public LocationController(IGeoService geoService)
        {
            this.geoService = geoService;
        }

        [Route("ip/location")]
        [HttpGet]
        public Location GetlocationByIp(string ip)
        {
            return geoService.findLocationByIp(ip);
        }


        [Route("city/locations")]
        [HttpGet]
        public List<Location> GetlocationByCity(string city)
        {
            return geoService.findLocationsByCityName(city);
        }
    }
}
