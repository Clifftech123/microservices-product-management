

using Microsoft.EntityFrameworkCore;
using ProductService.API.Models;

namespace ProductService.API.context
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) :  DbContext(options)
    {
      
      public DbSet<Product> Products { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
        base.OnModelCreating(modelBuilder);
         modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);

      }
        
    }
}