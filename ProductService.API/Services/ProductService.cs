using Microsoft.EntityFrameworkCore;
using ProductService.API.context;
using ProductService.API.Contracts;
using ProductService.API.Exceptions;
using ProductService.API.Interfaces;
using ProductService.API.Models;

namespace ProductService.API.Services
{
    /// <summary>
    /// Service for managing products.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ProductService"/> class.
    /// </remarks>
    /// <param name="context">The product database context.</param>
    public class ProductServiceImple(ProductDbContext context) : IProductService
  
    {
        private readonly ProductDbContext _context = context;


        /// <summary>
        /// Creates a new product asynchronously.
        /// </summary>
        /// <param name="request">The request containing product details.</param>
        /// <returns>The created product response.</returns>
        public async Task<ProductResponse> CreateProductAsync(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        /// <summary>
        /// Gets a product by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns>The product response if found; otherwise, null.</returns>
        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                 throw new ProductNotFoundException(id);
            }

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        /// <summary>
        /// Gets all products asynchronously.
        /// </summary>
        /// <returns>A collection of product responses.</returns>
        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
            return await _context.Products
                .Select(product => new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price
                })
                .ToListAsync();
        }

        /// <summary>
        /// Updates an existing product asynchronously.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="request">The request containing updated product details.</param>
        /// <returns>The updated product response if found; otherwise, null.</returns>
        public async Task<ProductResponse> UpdateProductAsync(int id, UpdateProductRequest request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
               throw new ProductNotFoundException(id);
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        /// <summary>
        /// Deletes a product by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns>True if the product was deleted; otherwise, false.</returns>
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
               throw new InvalidProductIdException(id);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
