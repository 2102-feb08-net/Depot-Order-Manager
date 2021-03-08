using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Library.Model;
using StoreApp.DataAccess.Repository;
using StoreApp.Web.Model;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.Web.Controllers
{
    /// <summary>
    /// A controller to manage customers.
    /// </summary>
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;

        /// <summary>
        /// Constructs a new CustomerController
        /// </summary>
        /// <param name="customerRepo"></param>
        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        /// <summary>
        /// Gets all of the customers in the database.
        /// </summary>
        /// <returns>Returns an enumerable of all of the customers in the database.</returns>
        [HttpGet("api/customers/getall")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepo.GetAllCustomers();
            var sortedCustomers = customers.OrderBy(c => c.Id).Select(c => new CustomerData(c));
            return Ok(sortedCustomers);
        }

        /// <summary>
        /// Searches the database for customers that contain the query in their first or last name.
        /// </summary>
        /// <param name="query">Name search query</param>
        /// <returns>Returns an enumerable of all of the customers with the query in their name.</returns>
        [HttpGet("api/customers/search")]
        public async Task<IActionResult> SearchCustomers(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Ok(await GetAllCustomers());

            var customers = await _customerRepo.SearchCustomersAsync(query);
            var sortedCustomers = customers.OrderBy(c => c.Id).Select(c => new CustomerData(c));
            return Ok(sortedCustomers);
        }

        [HttpPost("api/customers/add")]
        public async Task AddCustomer([Required] NewCustomer customer) => await _customerRepo.CreateCustomerAsync(customer);
    }
}