
namespace ProductService.API.Exceptions
{
  public class ProductUpdateConflictException : Exception
{
    public string ConflictMessage { get; }

    public ProductUpdateConflictException(string conflictMessage)
        : base($"Product update conflict: {conflictMessage}")
    {
        ConflictMessage = conflictMessage;
    }
}
}