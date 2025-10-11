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
    }
}
