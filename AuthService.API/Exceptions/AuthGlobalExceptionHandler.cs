using AuthService.API.Contracts;
using Microsoft.AspNetCore.Diagnostics;

namespace AuthService.API.Exceptions
{
    /// <summary>
    /// Global exception handler for the AuthService API.
    /// </summary>
    public class AuthGlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<AuthGlobalExceptionHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthGlobalExceptionHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger instance to log exceptions.</param>
        public AuthGlobalExceptionHandler(ILogger<AuthGlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Tries to handle the exception asynchronously.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="exception">The exception to handle.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
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
                    LoginException => StatusCodes.Status401Unauthorized,
                    RegisterException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError,
                }
            };

            httpContext.Response.StatusCode = errorResponse.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
            return true;
        }
    }
}
