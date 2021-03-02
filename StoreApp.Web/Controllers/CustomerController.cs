using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Library.Model;
using StoreApp.DataAccess.Repository;

namespace StoreApp.Web.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerRepository customerRepo = new CustomerRepository(Connection.CONNECTION_STRING, null);

        private const int PAGE_SIZE = 10;

        [HttpGet("api/customers/getall")]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = await customerRepo.GetAllCustomers();
            return customers.OrderBy(c => c.Id).Take(PAGE_SIZE);
        }

        [HttpPost("api/customers/add")]
        public void AddCustomer()
        {
            throw new System.NotImplementedException();
        }
    }
}