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
        private readonly ProductRepository productRepo = new ProductRepository(Connection.ConnectionString, Connection.Logger);

        [HttpGet("/api/products/getall")]
        public async Task<IEnumerable<IProduct>> GetAllProducts()
        {
            return await productRepo.GetAllProducts();
        }
    }
}