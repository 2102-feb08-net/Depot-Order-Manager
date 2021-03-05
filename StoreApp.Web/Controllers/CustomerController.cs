using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Library.Model;
using StoreApp.DataAccess.Repository;
using StoreApp.Web.Model;

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
        public async Task<IEnumerable<CustomerData>> GetAllCustomers()
        {
            var customers = await _customerRepo.GetAllCustomers();
            return customers.OrderBy(c => c.Id).Take(PAGE_SIZE).Select(c => new CustomerData(c));
        }

        [HttpGet("api/customers/search")]
        public async Task<IEnumerable<CustomerData>> SearchCustomers(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return await GetAllCustomers();

            var customers = await _customerRepo.SearchCustomersAsync(query);
            return customers.OrderBy(c => c.Id).Take(PAGE_SIZE).Select(c => new CustomerData(c));
        }

        [HttpPost("api/customers/add")]
        public async Task AddCustomer(NewCustomer customer) => await _customerRepo.CreateCustomerAsync(customer);
    }
}