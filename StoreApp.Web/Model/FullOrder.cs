using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Web.Model
{
    public class FullOrder
    {
        /// <summary>
        /// The head of the order with general information about the order.
        /// </summary>
        [Required]
        public OrderHead Head { get; init; }

        /// <summary>
        /// The orderlines of the order.
        /// </summary>
        [Required]
        [MinLength(1)]
        public List<IOrderLine> Lines { get; init; }

        /// <summary>
        /// The total price of the order.
        /// </summary>
        [Required]
        public decimal OrderTotalPrice { get; init; }
    }
}