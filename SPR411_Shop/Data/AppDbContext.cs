using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPR411_Shop.Models;

namespace SPR411_Shop.Data
{
    public class AppDbContext : IdentityDbContext<UserModel>
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<CartModel> CartItems { get; set; }

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

                e.Property(p => p.Rating)
                    .HasDefaultValue(0);

                e.Property(p => p.Amount)
                    .HasDefaultValue(0);
            });

            // Category
            builder.Entity<CategoryModel>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                e.Property(c => c.Icon)
                    .HasMaxLength(25);
            });

            // Cart
            builder.Entity<CartModel>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Count)
                .HasDefaultValue(1);
            });

            // Relationships
            builder.Entity<CategoryModel>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<CartModel>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Cart)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CartModel>()
                .HasOne(c => c.User)
                .WithMany(u => u.Cart)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
