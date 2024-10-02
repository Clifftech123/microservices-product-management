

namespace ProductService.API.Exceptions
{
  
 
  public class ProductNotFoundException : Exception
{
    public int NonExistentProductId { get; }

    public ProductNotFoundException(int nonExistentProductId)
        : base($"Product with ID {nonExistentProductId} not found.")
    {
        NonExistentProductId = nonExistentProductId;
    }
}
}