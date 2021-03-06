using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Web.Model
{
    public class OrderLine : IOrderLine
    {
        /// <summary>
        /// The product of the orderline
        /// </summary>
        [Required]
        public IProduct Product { get; set; }

        [Required]
        [Range(Order.MIN_QUANTITY_PER_ORDER, Order.MAX_QUANTITY_PER_ORDER)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal LineTotalPrice { get; set; }
    }
}