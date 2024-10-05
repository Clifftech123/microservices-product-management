using Microsoft.AspNetCore.Mvc;
using ProductService.API.Contracts;
using ProductService.API.Interfaces;

namespace ProductService.API.Controllers
{
    /// <summary>
    /// Controller for managing products.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;


        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="request">The product creation request.</param>
        /// <returns>The created product.</returns>
        [HttpPost]
        public async Task<ActionResult<ProductResponse>> CreateProduct(CreateProductRequest request)
        {
            var product = await _productService.CreateProductAsync(request);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        /// <summary>
        /// Gets a product by its identifier.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns>The product with the specified identifier.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="request">The product update request.</param>
        /// <returns>The updated product.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponse>> UpdateProduct(int id, UpdateProductRequest request)
        {
            var product = await _productService.UpdateProductAsync(id, request);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Deletes a product by its identifier.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns>No content if the product was deleted successfully.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}