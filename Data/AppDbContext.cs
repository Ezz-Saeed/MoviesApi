using Microsoft.EntityFrameworkCore;
using MoviesApiDevCreed.Model;

namespace MoviesApiDevCreed.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
