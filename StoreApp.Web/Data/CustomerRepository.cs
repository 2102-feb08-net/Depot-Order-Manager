using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Web.Models;

namespace StoreApp.Web.Data
{
    public class CustomerRepository
    {
        public List<Customer> Customers = new List<Customer>()
        {
            new Customer()
            {
                Id = 39,
                FirstName = "Mary",
                LastName = "Sue"
            },
            new Customer()
            {
                Id = 24,
                FirstName = "John",
                LastName = "Doe"
            },
            new Customer()
            {
                Id = 1,
                FirstName = "Bryson",
                LastName = "Ewell"
            }
        };
    }
}