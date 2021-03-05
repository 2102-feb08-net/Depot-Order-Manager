using StoreApp.Library;
using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    public class OrderLineTemplate : IOrderLineTemplate
    {
        /// <summary>
        /// The Id of the product.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; init; }

        /// <summary>
        /// The quantity ordered of the product.
        /// </summary>
        [Range(Order.MIN_QUANTITY_PER_ORDER, Order.MAX_QUANTITY_PER_ORDER)]
        public int Quantity { get; init; }
    }
}