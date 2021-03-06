using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    /// <summary>
    /// Constants for constraints on orders.
    /// </summary>
    public static class OrderConstraints
    {
        /// <summary>
        /// The minimum quantity of each product allowed on an orderline.
        /// </summary>
        public const int MIN_QUANTITY_PER_ORDERLINE = 1;

        /// <summary>
        /// The maximum quantity of each product allowed on an orderline.
        /// </summary>
        public const int MAX_QUANTITY_PER_ORDERLINE = 99;
    }
}