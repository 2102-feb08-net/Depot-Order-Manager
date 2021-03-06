using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [Range(1, int.MaxValue)]
        public int CustomerId { get; init; }

        /// <summary>
        /// The customer who made the order. It has just the name.
        /// </summary>
        [Required]
        public INewCustomer Customer { get; init; }

        /// <summary>
        /// The location that the order was placed from.
        /// </summary>
        [Required]
        public ILocationHead Location { get; init; }

        /// <summary>
        /// The time the order was proccessed.
        /// </summary>
        [Required]
        public string OrderTime { get; init; }

        /// <summary>
        /// The ID of the order.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int? Id { get; init; }

        /// <summary>
        /// The total price for the order.
        /// </summary>
        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal TotalPrice { get; init; }
    }
}