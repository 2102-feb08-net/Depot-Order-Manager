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
        private readonly ICustomerRepository _customerRepo;

        private const int PAGE_SIZE = 10;

        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet("api/customers/getall")]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = await _customerRepo.GetAllCustomers();
            return customers.OrderBy(c => c.Id).Take(PAGE_SIZE);
        }

        [HttpPost("api/customers/add")]
        public async Task AddCustomer(NewCustomer customer) => await _customerRepo.CreateCustomerAsync(customer);
    }
}