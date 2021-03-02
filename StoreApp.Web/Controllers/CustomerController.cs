using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Web.Models;
using StoreApp.Web.Data;

namespace StoreApp.Web.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerRepository customerRepo = new CustomerRepository();

        [HttpGet("api/customers/getall")]
        public IEnumerable<Customer> GetCustomers()
        {
            return customerRepo.Customers.OrderBy(c => c.Id).ToList();
        }

        [HttpPost("api/customers/add")]
        public void AddCustomer()
        {
            throw new System.NotImplementedException();
        }
    }
}