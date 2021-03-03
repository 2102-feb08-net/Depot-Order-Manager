using StoreApp.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Repository
{
    /// <summary>
    /// Repository for manipulation of Location data
    /// </summary>
    public interface ILocationRepository
    {
        /// <summary>
        /// Gets the location by a given name.
        /// </summary>
        /// <param name="name">The name of the location.</param>
        /// <returns>Returns the location with the given name.</returns>
        Task<ILocation> LookUpLocationByNameAsync(string name);

        /// <summary>
        /// Gets all of the locations in the database.
        /// </summary>
        /// <returns></returns>
        Task<List<Location>> GetLocationsAsync();
    }
}