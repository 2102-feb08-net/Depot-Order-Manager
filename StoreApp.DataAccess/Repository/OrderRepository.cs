using Microsoft.EntityFrameworkCore;
using StoreApp.Library;
using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Repository
{
    /// <summary>
    /// Repository for manipulation of Order data
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly DigitalStoreContext _context;

        /// <summary>
        /// Constructs a new Order Repository
        /// </summary>
        /// <param name="connectionString">The connection string to connect to the database.</param>
        /// <param name="logger">The logger to log the connection.</param>
        public OrderRepository(DigitalStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Sends and processes an order to the database using an IOrderTemplate.
        /// </summary>
        /// <param name="order">The order template to process</param>
        /// <returns>Returns an async task that completes when the transaction is complete</returns>
        public async Task SendOrderTransactionAsync(IOrderTemplate order)
        {
            if (order.OrderLines.Count == 0)
                throw new OrderException("Cannot submit order with no products in cart.");

            PurchaseOrder purchaseOrder = new PurchaseOrder()
            {
                CustomerId = order.CustomerId,
                DateProcessed = DateTime.Now,
                OrderLines = new List<OrderLine>(),
                StoreLocationId = order.StoreLocationId,
            };

            await AddProductsToOrder(order, _context, purchaseOrder);

            await _context.PurchaseOrders.AddAsync(purchaseOrder);

            await _context.SaveChangesAsync();
        }

        private static async Task AddProductsToOrder(IOrderTemplate order, DigitalStoreContext context, PurchaseOrder purchaseOrder)
        {
            var inventories = await context.Inventories
                .Include(i => i.Product)
                .Where(i => i.StoreId == purchaseOrder.StoreLocationId).ToListAsync();

            foreach (var orderLine in order.OrderLines)
            {
                if (orderLine.Quantity <= 0)
                    throw new OrderException("Cannot order products with a quantity less than or equal to 0.");

                var inventory = inventories.FirstOrDefault(i => i.ProductId == orderLine.ProductId);

                if (inventory is null)
                    throw new OrderException($"Store location does not contain the product ID '{orderLine.ProductId}' in its inventory.");

                if (inventory.Quantity >= orderLine.Quantity)
                    inventory.Quantity -= orderLine.Quantity;
                else
                    throw new OrderException($"The store location only has {inventory.Quantity} of '{inventory.Product.Name}' in stock, but the order is requesting to order {orderLine.Quantity} of the product.");

                purchaseOrder.OrderLines.Add(new OrderLine()
                {
                    Quantity = orderLine.Quantity,
                    Product = inventory.Product,
                    PurchaseOrder = purchaseOrder,
                    PurchaseUnitPrice = inventory.Product.UnitPrice,
                });
            }
            purchaseOrder.OrderTotalPrice = purchaseOrder.OrderLines.Sum(l => l.PurchaseUnitPrice * l.Quantity);
        }

        /// <summary>
        /// Retrieves a list of all of the orders from the specified customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Returns a list of all of the orders found</returns>
        public async Task<List<IReadOnlyOrder>> GetOrdersFromCustomerAsync(Library.Model.ICustomer customer)
        {
            var purchaseOrders = await _context.PurchaseOrders
                .Include(p => p.Customer)
                .Include(p => p.OrderLines)
                .ThenInclude(o => o.Product)
                .Include(p => p.StoreLocation)
                .ThenInclude(s => s.Address)
                .Where(p => p.Customer.Id == customer.Id).ToListAsync();

            var orderList = ConvertPurchaseOrderToIOrders(purchaseOrders);

            return orderList;
        }

        /// <summary>
        /// Retrieves a list of all of the orders from a specified location.
        /// </summary>
        /// <param name="locationName">The name of the location.</param>
        /// <returns>Returns a list of all of the orders found.</returns>
        public async Task<List<IReadOnlyOrder>> GetOrdersFromLocationAsync(string locationName)
        {
            var purchaseOrders = await _context.PurchaseOrders
                .Include(p => p.Customer)
                .Include(p => p.OrderLines)
                .ThenInclude(o => o.Product)
                .Include(p => p.StoreLocation)
                .ThenInclude(s => s.Address)
                .Where(p => p.StoreLocation.Name == locationName).ToListAsync();

            var orderList = ConvertPurchaseOrderToIOrders(purchaseOrders);
            return orderList;
        }

        private static List<IReadOnlyOrder> ConvertPurchaseOrderToIOrders(List<PurchaseOrder> purchaseOrders)
        {
            List<IReadOnlyOrder> orders = new List<IReadOnlyOrder>();
            foreach (var purchase in purchaseOrders)
            {
                ICustomer customer = new Library.Model.Customer(purchase.Customer.FirstName, purchase.Customer.LastName, purchase.CustomerId);

                ILocation location = new Location(
                    name: purchase.StoreLocation.Name,
                    address: purchase.StoreLocation.Address.ConvertAddress(),
                    inventory: new Dictionary<IProduct, int>(),
                    id: purchase.StoreLocation.Id
                );

                Dictionary<IProduct, int> productQuantities = new Dictionary<IProduct, int>();
                foreach (var line in purchase.OrderLines)
                {
                    IProduct product = new Library.Model.Product(line.Product.Name, line.Product.Category, line.PurchaseUnitPrice, line.Product.Id);
                    productQuantities.Add(product, line.Quantity);
                }

                var order = new ProcessedOrder(customer, location, productQuantities, purchase.DateProcessed, purchase.OrderTotalPrice, purchase.Id);
                orders.Add(order);
            }

            return orders;
        }

        /// <summary>
        /// Gets all of the processed orders in the database.
        /// </summary>
        /// <returns>Returns an IEnumerable of all of the orders as readonly</returns>
        public async Task<IEnumerable<IReadOnlyOrder>> GetAllProcessedOrdersAsync()
        {
            var purchaseOrders = await _context.PurchaseOrders
                                        .Include(p => p.Customer)
                                        .Include(p => p.OrderLines)
                                        .ThenInclude(o => o.Product)
                                        .Include(p => p.StoreLocation)
                                        .ThenInclude(s => s.Address).ToListAsync();
            return ConvertPurchaseOrderToIOrders(purchaseOrders);
        }

        /// <summary>
        /// Get all of the information about a single order.
        /// </summary>
        /// <param name="orderId">The Id of the order.</param>
        /// <returns>Returns the order with its information.</returns>
        public async Task<IReadOnlyOrder> GetOrderAsync(int orderId)
        {
            var orders = await _context.PurchaseOrders
                 .Include(p => p.Customer)
                 .Include(p => p.OrderLines)
                 .ThenInclude(o => o.Product)
                 .Include(p => p.StoreLocation)
                 .ThenInclude(s => s.Address)
                 .Where(o => o.Id == orderId).ToListAsync();
            return ConvertPurchaseOrderToIOrders(orders).FirstOrDefault();
        }

        public async Task<IEnumerable<IReadOnlyOrder>> SearchOrdersAsync(ISearchParams searchParams)
        {
            IQueryable<PurchaseOrder> orders = _context.PurchaseOrders
                 .Include(p => p.Customer)
                 .Include(p => p.OrderLines)
                 .ThenInclude(o => o.Product)
                 .Include(p => p.StoreLocation)
                 .ThenInclude(s => s.Address);

            if (searchParams.CustomerId.HasValue)
                orders = orders.Where(o => o.CustomerId == searchParams.CustomerId);

            if (searchParams.LocationId.HasValue)
                orders = orders.Where(o => o.StoreLocationId == searchParams.LocationId);

            var purchaseOrders = await orders.ToListAsync();

            return ConvertPurchaseOrderToIOrders(purchaseOrders);
        }
    }
}