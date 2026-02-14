using Microsoft.EntityFrameworkCore;
using SPR411_Shop.Models;

namespace SPR411_Shop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Product
            builder.Entity<ProductModel>(e =>
            {
                e.HasKey(p => p.Id);

                e.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                e.Property(p => p.Description)
                    .HasColumnType("ntext");

                e.Property(p => p.Image)
                    .HasMaxLength(100);

                e.Property(p => p.Price)
                    .HasColumnType("decimal(18, 2)");
            });

            // Category
            builder.Entity<CategoryModel>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // Relationships
            builder.Entity<CategoryModel>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(builder);
        }
    }
}
