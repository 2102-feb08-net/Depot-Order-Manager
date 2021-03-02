using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Web.Model
{
    public class OrderHead
    {
        /// <summary>
        /// The customer who made the order.
        /// </summary>
        public ICustomer Customer { get; init; }

        /// <summary>
        /// The location that the order was placed from.
        /// </summary>
        public ILocation StoreLocation { get; init; }

        /// <summary>
        /// The time the order was proccessed. Will always be null for this type.
        /// </summary>
        public string OrderTime { get; init; }

        /// <summary>
        /// The ID of the order. Will always be null for this type.
        /// </summary>
        public int? Id { get; init; }

        /// <summary>
        /// The total price for the order.
        /// </summary>
        public decimal TotalPrice { get; init; }
    }
}