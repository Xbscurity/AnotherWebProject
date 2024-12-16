using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFirstApp.Models;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка связи Product → ProductCategory
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category) // Один продукт имеет одну категорию
                .WithMany(c => c.Products) // Одна категория имеет много продуктов
                .HasForeignKey(p => p.CategoryId) // Внешний ключ
                .OnDelete(DeleteBehavior.Cascade); // Удаление каскадное
        }
    }
}
