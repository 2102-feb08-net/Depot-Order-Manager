using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// A template to place an order.
    /// </summary>
    public interface IOrderTemplate
    {
        /// <summary>
        /// The customer the order is for.
        /// </summary>
        int CustomerId { get; }

        /// <summary>
        /// The store the order is from.
        /// </summary>
        int StoreLocationId { get; }

        /// <summary>
        /// The orderlines in the order.
        /// </summary>
        List<OrderLineTemplate> OrderLines { get; }
    }
}