
namespace ProductService.API.Exceptions
{
   public class InvalidProductIdException : Exception
{
    public int InvalidProductId { get; }

    public InvalidProductIdException(int invalidProductId)
        : base($"Invalid product ID: {invalidProductId}")
    {
        InvalidProductId = invalidProductId;
    }
}

}