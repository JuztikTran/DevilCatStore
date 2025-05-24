using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductVarriant> ProductVarriants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<CategoryItem>().ToTable("CategoryItem");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ProductImages>().ToTable("ProductImages");
            modelBuilder.Entity<ProductVarriant>().ToTable("ProductVarriant");

            modelBuilder.Entity<CategoryItem>()
                .HasKey(ci => new { ci.CategoryID, ci.ProductID });

            modelBuilder.Entity<CategoryItem>()
                .HasOne(ci => ci.Product)
                .WithMany(c => c.CategoryItems)
                .HasForeignKey(ci => ci.ProductID);

            modelBuilder.Entity<CategoryItem>()
                .HasOne(ci => ci.Category)
                .WithMany(c => c.CategoryItems)
                .HasForeignKey(ci => ci.CategoryID);
        }
    }
}
