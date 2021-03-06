using StoreApp.Library;
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
        [Range(OrderConstraints.MIN_QUANTITY_PER_ORDERLINE, OrderConstraints.MAX_QUANTITY_PER_ORDERLINE)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal LineTotalPrice { get; set; }
    }
}