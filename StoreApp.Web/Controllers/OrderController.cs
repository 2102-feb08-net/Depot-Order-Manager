using Microsoft.AspNetCore.Mvc;
using StoreApp.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Library.Model;
using StoreApp.Web.Model;
using StoreApp.Library;

namespace StoreApp.Web.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository orderRepo = new OrderRepository(Connection.ConnectionString, null);

        [HttpGet("/api/orders/getall")]
        public async Task<IEnumerable<OrderHead>> GetOrders()
        {
            var orders = await orderRepo.GetAllProcessedOrders();
            return orders.Select(o => new OrderHead()
            {
                Id = o.Id,
                Customer = o.Customer,
                StoreLocation = o.StoreLocation,
                OrderTime = o.OrderTime.HasValue ? o.OrderTime.Value.DateTime.ToString("D") : "No Data",
                TotalPrice = OrderProcessor.CalculateTotalPrice(o)
            });
        }
    }
}