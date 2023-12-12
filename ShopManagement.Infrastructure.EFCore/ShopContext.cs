using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EFCore.Mapping;


namespace ShopManagement.Infrastructure.EFCore
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; }
        public DbSet<ProductCategory> ProductCategories { get; }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) 
        {
            ProductCategories = Set<ProductCategory>();
            Products = Set<Product>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var productCategoryAssembly = typeof(ProductCategoryMapping).Assembly;
            var productAssembly = typeof(ProductCategory).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(productCategoryAssembly);
            modelBuilder.ApplyConfigurationsFromAssembly(productAssembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
