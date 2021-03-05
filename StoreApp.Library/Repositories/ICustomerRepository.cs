using StoreApp.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Repository
{
    /// <summary>
    /// Repository for manipulation of Customer data
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Creates and sends a request to the server to get the customer entry with the specified first and last name
        /// </summary>
        /// <param name="firstName">The first name of the customer</param>
        /// <param name="lastName">The last name of the customer</param>
        /// <returns>Returns a list of customers as some customers may have the same name</returns>
        Task<IEnumerable<ICustomer>> LookUpCustomersByNameAsync(string firstName, string lastName);

        /// <summary>
        /// Searches the database for customers that have either their first or last name contain the search query.
        /// </summary>
        /// <param name="nameQuery">The query to search for in the customers' full names</param>
        /// <returns>Returns a list of customers whose names contain the query.</returns>
        Task<IEnumerable<ICustomer>> SearchCustomersAsync(string nameQuery);

        /// <summary>
        /// Creates a customer and adds it into the database. It does not check for duplicates before creating it.
        /// </summary>
        /// <param name="firstName">The first name of the customer.</param>
        /// <param name="lastName">The last name of the customer.</param>
        Task CreateCustomerAsync(INewCustomer customer);

        /// <summary>
        /// Gets all of the customers in the database. This should only be called if you plan on doing additional querying to get a subset of the data.
        /// </summary>
        /// <returns>Returns an IEnumerable with all of the customers in the database.</returns>
        Task<IEnumerable<Customer>> GetAllCustomers();
    }
}