
using Microsoft.EntityFrameworkCore;
using ProductService.API.Models;

namespace ProductService.API.Configurations
{
    public class ProductTypeConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
           builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");


            builder.HasData (
                new Product { Id = 1, Name = "Keyboard", Description = "A mechanical keyboard", Price = 50.00m },
                new Product { Id = 2, Name = "Mouse", Description = "A wireless mouse", Price = 20.00m },
                new Product { Id = 3, Name = "Monitor", Description = "A 4k monitor", Price = 300.00m },
                new Product { Id = 4, Name = "Laptop", Description = "A gaming laptop", Price = 800.00m },
                new Product { Id = 5, Name = "Headset", Description = "A wireless headset", Price = 100.00m }
                
            );

        }
    }
}