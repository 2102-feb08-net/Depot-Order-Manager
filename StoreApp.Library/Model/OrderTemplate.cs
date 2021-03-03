using StoreApp.Library;
using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    public class OrderTemplate : IOrderTemplate
    {
        public int CustomerId { get; init; }

        public int StoreLocationId { get; init; }

        public List<OrderLineTemplate> OrderLines { get; init; }
    }
}