using Microsoft.EntityFrameworkCore;
using WebAPI.Server.Model;

namespace WebAPI.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        //Veritabınında Categories adında bir tablo oluşturulmasını sağlar.
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        // Seed data eklemek için OnModelCreating metodunu override edelim
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category için örnek veri (isterseniz ekleyebilirsiniz)
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Elektronik" },
                new Category { Id = 2, Name = "Giyim" }
            );

            // Product için seed data
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 25000.00m, Stock = 10 },
                new Product { Id = 2, Name = "Klavye", Price = 750.50m, Stock = 50 },
                new Product { Id = 3, Name = "Mouse", Price = 400.00m, Stock = 75 }
            );
        }
    }
}
