using AuthService.API.Context;
using AuthService.API.Data;
using AuthService.API.Exceptions;
using AuthService.API.Interface;
using AuthService.API.Models;
using AuthService.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthService.API.Extensions
{
    public static class ServiceExtensions
    {
        private const string ConnectionStringName = "ConnectionString";
        private const string JwtOptionsSectionName = "JwtOptions";

        public static void ConfigureAllServices(this IServiceCollection services, IConfiguration config, IHostApplicationBuilder builder)
        {
            // Configure database context
            var connectionString = config.GetConnectionString(ConnectionStringName);
            services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));

            // Configure identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

            // Configure business services
            services.AddScoped<IJwtTokenGenerator, JwtTokenGeneratorServices>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // Configure JWT authentication
            var jwtSection = config.GetSection(JwtOptionsSectionName);
            services.Configure<JwtOptions>(jwtSection);
            var jwtOptions = jwtSection.Get<JwtOptions>();
            var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.SaveToken = true;
                   options.TokenValidationParameters = tokenValidationParameters;
               });

            // Add problem details and exception handling
            services.AddProblemDetails();
            services.AddExceptionHandler<AuthGlobalExceptionHandler>();
        }
    }
}
