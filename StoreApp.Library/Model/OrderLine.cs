using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;

namespace StoreApp.Library.Model
{
    public class OrderLine : IOrderLine
    {
        public IProduct Product { get; }

        public int Quantity { get; }

        public decimal LineTotalPrice => Product.UnitPrice * Quantity;

        public OrderLine(IProduct product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));

            if (quantity is (< OrderConstraints.MIN_QUANTITY_PER_ORDERLINE or > OrderConstraints.MAX_QUANTITY_PER_ORDERLINE))
                throw new ArgumentException(message: "The quantity of an orderline must be in bounds.", paramName: nameof(quantity));
            Quantity = quantity;
        }
    }
}