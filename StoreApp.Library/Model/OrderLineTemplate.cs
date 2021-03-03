using StoreApp.Library;
using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    public class OrderLineTemplate : IOrderLineTemplate
    {
        public int ProductId { get; init; }

        public int Quantity { get; init; }
    }
}