using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Web.Model
{
    public class OrderLine : IOrderLine
    {
        public IProduct Product { get; set; }

        public int Quantity { get; set; }

        public decimal LineTotalPrice { get; set; }
    }
}