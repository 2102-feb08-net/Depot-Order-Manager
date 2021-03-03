using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Web.Model
{
    public class FullOrder
    {
        public OrderHead Head { get; init; }

        public List<IOrderLine> Lines { get; init; }

        public decimal OrderTotalPrice { get; init; }
    }
}