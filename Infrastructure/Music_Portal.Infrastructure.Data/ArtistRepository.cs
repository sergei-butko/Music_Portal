using System.Collections.Generic;
using System.Linq;
using Music_Portal.Domain.Core;
using Music_Portal.Domain.Interfaces;

namespace Music_Portal.Infrastructure.Data
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ApplicationContext _db;

        public ArtistRepository(ApplicationContext context)
        {
            _db = context;
        }

        public IEnumerable<Artist> GetArtistsEnumerable()
        {
            return _db.Artists;
        }

        public Artist GetArtist(string name)
        {
            return _db.Artists.FirstOrDefault(a => a.Name == name);
        }

        public void Create(Artist artist)
        {
            _db.Artists.Add(artist);
            _db.SaveChanges();
        }

        public void Update(Artist artist)
        {
            var artistToUpdate = _db.Artists.FirstOrDefault(a => a.Id == artist.Id);
            if (artistToUpdate != null)
            {
                artistToUpdate.Listeners = artist.Listeners;
                artistToUpdate.Playcount = artist.Playcount;
                _db.SaveChanges();
            }
        }
        
        public void Delete(int id)
        {
            Artist artist = _db.Artists.FirstOrDefault(a => a.Id == id);
            if (artist != null)
            {
                _db.Artists.Remove(artist);
            }
            _db.SaveChanges();
        }
    }
}