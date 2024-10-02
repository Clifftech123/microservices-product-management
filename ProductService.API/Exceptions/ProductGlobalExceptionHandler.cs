using Microsoft.AspNetCore.Diagnostics;
using ProductService.API.Contracts;

namespace ProductService.API.Exceptions
{
    /// <summary>
    /// Handles global exceptions for the ProductService API.
    /// </summary>
    public class ProductGlobalExceptionHandler(ILogger<ProductGlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<ProductGlobalExceptionHandler> _logger = logger;

        /// <summary>
        /// Attempts to handle the given exception and write an appropriate error response.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <param name="exception">The exception that occurred.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the exception was handled.</returns>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An unhandled exception has occurred: {Message}", exception.Message);

            var errorResponse = new ErrorResponse
            {
                Message = exception.Message,
                Title = exception.GetType().Name,
                StatusCode = exception switch
                {
                    ProductNotFoundException => StatusCodes.Status404NotFound,
                    ProductUpdateConflictException => StatusCodes.Status400BadRequest,
                    InvalidProductIdException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError,
                }
            };

            httpContext.Response.StatusCode = errorResponse.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}
