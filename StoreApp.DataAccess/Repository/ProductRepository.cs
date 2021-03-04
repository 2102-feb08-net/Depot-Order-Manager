using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using StoreApp.Library.Model;
using StoreApp.Library;

namespace StoreApp.DataAccess.Repository
{
    /// <summary>
    /// Repository for manipulation of Product data
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly DigitalStoreContext _context;

        /// <summary>
        /// Constructs a new Product Repository
        /// </summary>
        /// <param name="connectionString">The connection string to connect to the database.</param>
        /// <param name="logger">The logger to log the connection.</param>
        public ProductRepository(DigitalStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Looks up a product by its name and returns it.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <returns>Returns a single product with the specified name.</returns>
        public async Task<IProduct> LookupProductFromName(string name)
        {
            var product = await _context.Products.Where(c => c.Name == name).FirstOrDefaultAsync();

            if (product is not null)
                return new Library.Model.Product(product.Name, product.Category, product.UnitPrice, product.Id);
            else
                return null;
        }

        /// <summary>
        /// Gets all of the products in the database
        /// </summary>
        /// <returns>Returns an IEnumerable of IProducts</returns>
        public async Task<IEnumerable<IProduct>> GetAllProducts() => await _context.Products.Select(p => new Library.Model.Product(p.Name, p.Category, p.UnitPrice, p.Id)).ToListAsync();
    }
}