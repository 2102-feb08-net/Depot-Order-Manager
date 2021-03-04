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
        /// <remarks>Uses int fo customer Id instead of ICustomer because there is some issues with serializing inherited interfaces</remarks>
        public int CustomerId { get; init; }

        /// <summary>
        /// The customer who made the order. It has just the name.
        /// </summary>
        public INewCustomer Customer { get; init; }

        /// <summary>
        /// The location that the order was placed from.
        /// </summary>
        public ILocationHead Location { get; init; }

        /// <summary>
        /// The time the order was proccessed.
        /// </summary>
        public string OrderTime { get; init; }

        /// <summary>
        /// The ID of the order.
        /// </summary>
        public int? Id { get; init; }

        /// <summary>
        /// The total price for the order.
        /// </summary>
        public decimal TotalPrice { get; init; }
    }
}