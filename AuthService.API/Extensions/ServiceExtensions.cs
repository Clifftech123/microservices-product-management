using AuthService.API.Context;
using AuthService.API.Exceptions;
using AuthService.API.Interface;
using AuthService.API.Services;
using Microsoft.EntityFrameworkCore;

namespace AuthService.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAllServices(this IHostApplicationBuilder builder)
        {
            // Configure database context
            builder.Services.AddDbContext<AuthDbContext>(configure =>
            {
                configure.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
            });
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGeneratorServices>();

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            // Add problem details and exception handling
            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<AuthGlobalExceptionHandler>();

        }
    }
}
