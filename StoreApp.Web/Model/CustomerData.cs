using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Web.Model
{
    /// <summary>
    /// Customer data made to serialize as JSON does not serialize inherited interfaces.
    /// </summary>
    public class CustomerData
    {
        /// <summary>
        /// Id of the customer
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The first name of the customer.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// The last name of the customer.
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// Constructs a new customer data from an ICustomer
        /// </summary>
        /// <param name="customer"></param>
        public CustomerData(ICustomer customer)
        {
            Id = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
        }
    }
}