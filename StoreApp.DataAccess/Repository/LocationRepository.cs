using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.DataAccess.Repository
{
    /// <summary>
    /// Repository for manipulation of Location data
    /// </summary>
    public class LocationRepository : ILocationRepository
    {
        private readonly DigitalStoreContext _context;

        /// <summary>
        /// Constructs a new Location Repository
        /// </summary>
        /// <param name="connectionString">The connection string to connect to the database.</param>
        /// <param name="logger">The logger to log the connection.</param>
        public LocationRepository(DigitalStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the location by a given name.
        /// </summary>
        /// <param name="name">The name of the location.</param>
        /// <returns>Returns the location with the given name.</returns>
        public async Task<Library.Model.ILocation> LookUpLocationByNameAsync(string name)
        {
            var storeLocation = await _context.StoreLocations
                .Include(s => s.Inventories)
                .ThenInclude(i => i.Product)
                .Include(s => s.Address)
                .FirstOrDefaultAsync(s => s.Name == name);

            if (storeLocation is null)
                return null;

            var inventoryPairs = storeLocation.Inventories.Select(
                i => new KeyValuePair<Library.Model.IProduct, int>(
                    new Library.Model.Product(i.Product.Name, i.Product.Category, i.Product.UnitPrice, i.ProductId),
                    i.Quantity)).ToList();

            var inventoryDictionary = inventoryPairs.ToDictionary((keyItem) => (keyItem).Key, (valueItem) => valueItem.Value);

            Library.Model.ILocation location = new Library.Model.Location(
                name: storeLocation.Name,
                address: storeLocation.Address.ConvertAddress(),
                inventory: inventoryDictionary,
                id: storeLocation.Id
            );

            return location;
        }

        public async Task<List<Library.Model.Location>> GetLocationsAsync()
        {
            return await _context.StoreLocations.Select(l => new Library.Model.Location(
                l.Name,
                l.Address.ConvertAddress(),
                new Dictionary<Library.Model.IProduct, int>(),
                l.Id
            )).ToListAsync();
        }
    }
}