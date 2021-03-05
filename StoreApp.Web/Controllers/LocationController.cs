using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Library.Model;
using StoreApp.DataAccess.Repository;
using StoreApp.Web.Model;

namespace StoreApp.Web.Controllers
{
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepo;

        public LocationController(ILocationRepository locationRepo)
        {
            _locationRepo = locationRepo;
        }

        [HttpGet("api/locations/getall")]
        public async Task<IEnumerable<LocationHead>> GetAllLocations()
        {
            var locations = await _locationRepo.GetLocationsAsync();
            return locations.OrderBy(l => l.Id).Select(l => new LocationHead()
            {
                Id = l.Id,
                Name = l.Name,
                AddressLines = l.Address.FormatToMultiline()
            });
        }

        [HttpGet("api/locations/search")]
        public async Task<IEnumerable<LocationHead>> SearchCustomers(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return await GetAllLocations();

            var locations = await _locationRepo.SearchLocationsAsync(query);
            return locations.OrderBy(l => l.Id).Select(l => new LocationHead()
            {
                Id = l.Id,
                Name = l.Name,
                AddressLines = l.Address.FormatToMultiline()
            });
        }
    }
}