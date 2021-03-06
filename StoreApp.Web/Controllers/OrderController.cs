using Microsoft.AspNetCore.Mvc;
using StoreApp.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Library.Model;
using StoreApp.Web.Model;
using StoreApp.Library;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.Web.Controllers
{
    /// <summary>
    /// A controller to manage orders.
    /// </summary>
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;

        /// <summary>
        /// Constructs a new OrderController
        /// </summary>
        /// <param name="orderRepo">The order repository to use.</param>
        public OrderController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        /// <summary>
        /// Get the basic information on every order.
        /// </summary>
        /// <returns>Returns an enumerable with gener</returns>
        [HttpGet("/api/orders/getall")]
        public async Task<IEnumerable<OrderHead>> GetOrders()
        {
            var orders = await _orderRepo.GetAllProcessedOrdersAsync();
            return orders.Select(o => ConvertOrderToOnlyHead(o));
        }

        /// <summary>
        /// Get all of the information about a single order with the specified Id.
        /// </summary>
        /// <param name="id">The Id of the order.</param>
        /// <returns>Returns all of the information on the order.</returns>
        [HttpGet("/api/orders/{id}")]
        public async Task<FullOrder> GetOrderDetails(int id)
        {
            var order = await _orderRepo.GetOrderAsync(id);
            var head = ConvertOrderToOnlyHead(order);
            var lines = order.OrderLines;

            return new FullOrder()
            {
                Head = head,
                Lines = lines,
            };
        }

        /// <summary>
        /// Searches orders for orders with the specified customer and location.
        /// </summary>
        /// <param name="customer">The customer Id to search for.</param>
        /// <param name="location">The location Id to search for.</param>
        /// <returns>Returns the orders found. If no parameters, then all orders are returned.</returns>
        [HttpGet("/api/orders/search")]
        public async Task<IEnumerable<OrderHead>> SearchOrders(int? customer, int? location)
        {
            ISearchParams searchParams = new SearchParams() { CustomerId = customer, LocationId = location };
            var orders = await _orderRepo.SearchOrdersAsync(searchParams);
            return orders.Select(o => ConvertOrderToOnlyHead(o));
        }

        /// <summary>
        /// Sends an order to the server with the specified template.
        /// </summary>
        /// <param name="orderTemplate">The order template to send to the server/</param>
        [HttpPost("/api/orders/send-order")]
        public async Task SendOrder([Required] OrderTemplate orderTemplate) => await _orderRepo.SendOrderTransactionAsync(orderTemplate);

        private static OrderHead ConvertOrderToOnlyHead(IReadOnlyOrder order)
        {
            return new OrderHead()
            {
                Id = order.Id,
                CustomerId = order.Customer.Id,
                Customer = order.Customer,
                Location = new LocationHead() { Id = order.StoreLocation.Id, Name = order.StoreLocation.Name, AddressLines = order.StoreLocation.Address.FormatToMultiline() },
                OrderTime = order.OrderTime.HasValue ? order.OrderTime.Value.DateTime.ToString("D") : "No Data",
                TotalPrice = order.TotalPrice
            };
        }
    }
}