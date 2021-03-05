using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// An order than has already been processed and entered into the database.
    /// </summary>
    public class ProcessedOrder : IReadOnlyOrder
    {
        /// <summary>
        /// The customer who made the order.
        /// </summary>
        public ICustomer Customer { get; }

        /// <summary>
        /// The location that the order was placed from.
        /// </summary>
        public ILocation StoreLocation { get; }

        /// <summary>
        /// The products in the order and their corresponding quantities.
        /// </summary>
        public IReadOnlyDictionary<IProduct, int> ShoppingCartQuantity { get; }

        /// <summary>
        /// The time the order was proccessed.
        /// </summary>
        public DateTimeOffset? OrderTime { get; }

        /// <summary>
        /// The ID of the order. It cannot be null on a processed order.
        /// </summary>
        public int? Id { get; }

        /// <summary>
        /// The total price for the order.
        /// </summary>
        public decimal TotalPrice { get; }

        /// <summary>
        /// Constructs a new Processed Order.
        /// </summary>
        /// <param name="customer">The customer in the order</param>
        /// <param name="storeLocation">The store location the order was sent to</param>
        /// <param name="productQuantities">The quantites of each product in the order</param>
        /// <param name="orderTime">The date and time of the order</param>
        /// <param name="id">The order number id</param>
        public ProcessedOrder(ICustomer customer, ILocation storeLocation, IDictionary<IProduct, int> productQuantities, DateTimeOffset orderTime, decimal totalPrice, int id)
        {
            if (orderTime > DateTime.Now)
                throw new ArgumentException(message: "Order time cannot be in the future.", paramName: nameof(orderTime));

            if (orderTime == default)
                throw new ArgumentException(message: "Order time must be set.", paramName: nameof(orderTime));

            if (totalPrice < 0)
                throw new ArgumentException(message: "Total price of an order cannot be negative.", paramName: nameof(totalPrice));

            if (id <= 0)
                throw new ArgumentException(message: "ID must be greater than 0.", paramName: nameof(id));

            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            StoreLocation = storeLocation ?? throw new ArgumentNullException(nameof(storeLocation));
            ShoppingCartQuantity = (IReadOnlyDictionary<IProduct, int>)productQuantities ?? throw new ArgumentNullException(nameof(productQuantities));
            OrderTime = orderTime;
            TotalPrice = totalPrice;
            Id = id;
        }
    }
}