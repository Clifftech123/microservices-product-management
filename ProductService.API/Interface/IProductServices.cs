using ProductService.API.Contracts;

namespace ProductService.API.Interfaces
{
    /// <summary>
    /// Interface for product service operations.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="request">The request containing product details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created product response.</returns>
        Task<ProductResponse> CreateProductAsync(CreateProductRequest request);

        /// <summary>
        /// Gets a product by its identifier.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the product response.</returns>
        Task<ProductResponse> GetProductByIdAsync(int id);

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of product responses.</returns>
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync();

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param="id">The product identifier.</param>
        /// <param name="request">The request containing updated product details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated product response.</returns>
        Task<ProductResponse> UpdateProductAsync(int id, UpdateProductRequest request);

        /// <summary>
        /// Deletes a product by its identifier.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        Task<bool> DeleteProductAsync(int id);
    }
}
