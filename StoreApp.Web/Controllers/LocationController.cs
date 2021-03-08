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
    /// <summary>
    /// A controller to manage store location information.
    /// </summary>
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepo;

        /// <summary>
        /// Constructs a new LocationController
        /// </summary>
        /// <param name="locationRepo"></param>
        public LocationController(ILocationRepository locationRepo)
        {
            _locationRepo = locationRepo;
        }

        /// <summary>
        /// Gets all of the store locations in the database.
        /// </summary>
        /// <returns>Returns an enumerable of all of the locations.</returns>
        [HttpGet("api/locations/getall")]
        public async Task<IActionResult> GetAllLocations()
        {
            var locations = await _locationRepo.GetLocationsAsync();
            var sortedLocations = locations.OrderBy(l => l.Id).Select(l => new LocationHead()
            {
                Id = l.Id,
                Name = l.Name,
                AddressLines = l.Address.FormatToMultiline()
            });
            return Ok(sortedLocations);
        }

        /// <summary>
        /// Searches the database for store locations that contain the query in their name.
        /// </summary>
        /// <param name="query">Name search query</param>
        /// <returns>Returns an enumerable of all of the locations with the query in their name. An empty query returns all locations.</returns>
        [HttpGet("api/locations/search")]
        public async Task<IActionResult> SearchLocations(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Ok(await GetAllLocations());

            var locations = await _locationRepo.SearchLocationsAsync(query);
            var sortedLocations = locations.OrderBy(l => l.Id).Select(l => new LocationHead()
            {
                Id = l.Id,
                Name = l.Name,
                AddressLines = l.Address.FormatToMultiline()
            });

            return Ok(sortedLocations);
        }
    }
}