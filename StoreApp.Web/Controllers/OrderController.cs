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
        private readonly IOrderRepository _orderRepo;

        public OrderController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpGet("/api/orders/getall")]
        public async Task<IEnumerable<OrderHead>> GetOrders()
        {
            var orders = await _orderRepo.GetAllProcessedOrdersAsync();
            return orders.Select(o => ConvertOrderToOnlyHead(o));
        }

        [HttpGet("/api/orders/{id}")]
        public async Task<FullOrder> GetOrderDetails(int id)
        {
            var order = await _orderRepo.GetOrderAsync(id);
            var head = ConvertOrderToOnlyHead(order);
            var lines = ConvertOrderToOrderLines(order);

            return new FullOrder()
            {
                Head = head,
                Lines = lines,
                OrderTotalPrice = lines.Sum(l => l.LineTotalPrice)
            };
        }

        [HttpPost("/api/orders/send-order")]
        public async Task SendOrder(OrderTemplate orderTemplate) => await _orderRepo.SendOrderTransactionAsync(orderTemplate);

        private static OrderHead ConvertOrderToOnlyHead(IReadOnlyOrder order)
        {
            return new OrderHead()
            {
                Id = order.Id,
                CustomerId = order.Customer.Id,
                Customer = order.Customer,
                Location = new LocationHead() { Id = order.StoreLocation.Id, Name = order.StoreLocation.Name, AddressLines = order.StoreLocation.Address.FormatToMultiline() },
                OrderTime = order.OrderTime.HasValue ? order.OrderTime.Value.DateTime.ToString("D") : "No Data",
                TotalPrice = OrderProcessor.CalculateTotalPrice(order)
            };
        }

        private static List<IOrderLine> ConvertOrderToOrderLines(IReadOnlyOrder order)
        {
            List<IOrderLine> lines = new List<IOrderLine>();
            foreach (var line in order.ShoppingCartQuantity)
            {
                lines.Add(new OrderLine()
                {
                    Product = line.Key,
                    Quantity = line.Value,
                    LineTotalPrice = line.Key.UnitPrice * line.Value, //TODO: Refactor into business logic
                });
            }
            return lines;
        }
    }
}