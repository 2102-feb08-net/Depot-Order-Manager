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
        public IProduct Product { get; set; }

        [Range(Order.MIN_QUANTITY_PER_ORDER, Order.MAX_QUANTITY_PER_ORDER)]
        public int Quantity { get; set; }

        public decimal LineTotalPrice { get; set; }
    }
}