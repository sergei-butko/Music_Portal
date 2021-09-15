using Microsoft.EntityFrameworkCore;

namespace Domain.Core
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Artist> Artists { get; set; }
    }
}