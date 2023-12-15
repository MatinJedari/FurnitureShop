using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore.Mapping;


namespace ShopManagement.Infrastructure.EFCore
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; }
        public DbSet<ProductCategory> ProductCategories { get; }
        public DbSet<ProductPicture> ProductPictures { get; }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) 
        {
            ProductCategories = Set<ProductCategory>();
            Products = Set<Product>();
            ProductPictures = Set<ProductPicture>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var productCategoryAssembly = typeof(ProductCategoryMapping).Assembly;
            var productAssembly = typeof(ProductMapping).Assembly;
            var productPictureAssembly = typeof(ProductPictureMapping).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(productCategoryAssembly);
            modelBuilder.ApplyConfigurationsFromAssembly(productAssembly);
            modelBuilder.ApplyConfigurationsFromAssembly(productPictureAssembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
