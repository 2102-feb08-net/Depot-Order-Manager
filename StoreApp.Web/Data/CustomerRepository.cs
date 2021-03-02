using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Library.Model;

namespace StoreApp.Web.Data
{
    public class CustomerRepository
    {
        public List<Customer> Customers = new List<Customer>()
        {
            new Customer("Mary", "Sue", 39),
            new Customer("John", "Doe", id: 24),
            new Customer("Bryson", "Ewell", id: 1)
        };
    }
}