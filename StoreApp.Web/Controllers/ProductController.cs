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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        /// <summary>
        /// Gets an enumerable of all of the products in the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/products/getall")]
        public async Task<IEnumerable<IProduct>> GetAllProducts() => await _productRepo.GetAllProducts();
    }
}