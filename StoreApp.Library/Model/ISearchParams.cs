using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// An interface to allow passing of many search parameters at once.
    /// </summary>
    public interface ISearchParams
    {
        /// <summary>
        /// The Id of the customer.
        /// </summary>
        int? CustomerId { get; }

        /// <summary>
        /// The Id of the store location.
        /// </summary>
        int? LocationId { get; }
    }
}