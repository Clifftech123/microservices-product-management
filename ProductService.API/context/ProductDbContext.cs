using Microsoft.EntityFrameworkCore;
using ProductService.API.Models;

namespace ProductService.API.Context
{
    /// <summary>
    /// Represents the database context for products.
    /// </summary>
    public class ProductDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// exposed in <see cref="DbSet{TEntity}"/> properties on your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
        }
    }
}
