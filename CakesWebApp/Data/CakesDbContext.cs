using CakesWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CakesWebApp.Data
{
    public class CakesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrdersProducts> OrdersProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=YANEV-PC\\SQLEXPRESS;Database=Cakes;Integrated Security=true");
            //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Cakes;Integrated Security=True;");
        }
    }
}
