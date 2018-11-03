using IRunes_HomeEdition.Models;
using Microsoft.EntityFrameworkCore;

namespace IRunes_HomeEdition.Data
{
    public class IRunesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=YANEV-PC\\SQLEXPRESS;Database=IRunes_HomeEdition;Integrated Security=true");
        }
    }
}