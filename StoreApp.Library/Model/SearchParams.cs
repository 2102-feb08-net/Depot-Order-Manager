using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// An object to allow passing of many search parameters at once.
    /// </summary>
    public record SearchParams : ISearchParams
    {
        /// <summary>
        /// The Id of the customer.
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// The Id of the store location.
        /// </summary>
        public int? LocationId { get; set; }
    }
}