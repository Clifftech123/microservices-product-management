using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductService.API.Context;
using ProductService.API.Exceptions;
using ProductService.API.Interfaces;
using ProductService.API.Services;
using System.Reflection;

namespace ProductService.API.Extensions
{
    /// <summary>
    /// Provides extension methods for adding product services.
    /// </summary>
    public static class ProductServiceExtensions
    {
        /// <summary>
        /// Adds product service extensions to the application builder.
        /// </summary>
        /// <param name="builder">The host application builder.</param>
        public static void AddProductServiceExtensions(this IHostApplicationBuilder builder)
        {

           var configuration = builder.Configuration;
            // Adding of the database context
            builder.Services.AddDbContext<ProductDbContext>(configure =>
            {
                configure.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Services.AddExceptionHandler<ProductGlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            builder.Services.AddScoped<IProductService, ProductServiceImple>();
        }
    }
}
