using Microsoft.EntityFrameworkCore;

namespace Music_Portal.Domain.Core
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<SimilarArtistRelations> SimilarArtistsToArtist { get; set; }
    }
}