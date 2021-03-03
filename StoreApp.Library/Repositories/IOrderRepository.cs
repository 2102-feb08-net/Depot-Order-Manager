using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    /// <summary>
    /// Repository for manipulation of Order data
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Retrieves all of the orders from the specified customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Returns a list of all orders found</returns>
        Task<List<IReadOnlyOrder>> GetOrdersFromCustomerAsync(ICustomer customer);

        /// <summary>
        /// Retrieves all of the orders from the specified location name.
        /// </summary>
        /// <param name="locationName"></param>
        /// <returns>Returns a list of all orders found</returns>
        Task<List<IReadOnlyOrder>> GetOrdersFromLocationAsync(string locationName);

        /// <summary>
        /// Sends and processes an order to the database using an IOrderTemplate.
        /// </summary>
        /// <param name="order">The order template to process</param>
        /// <returns>Returns an async task that completes when the transaction is complete</returns>
        Task SendOrderTransactionAsync(IOrderTemplate order);

        /// <summary>
        /// Gets all of the processed orders in the database.
        /// </summary>
        /// <returns>Returns an IEnumerable of all of the orders as readonly</returns>
        Task<IEnumerable<IReadOnlyOrder>> GetAllProcessedOrdersAsync();

        /// <summary>
        /// Get all of the information about a single order.
        /// </summary>
        /// <param name="orderId">The Id of the order.</param>
        /// <returns>Returns the order with its information.</returns>
        Task<IReadOnlyOrder> GetOrderAsync(int orderId);
    }
}