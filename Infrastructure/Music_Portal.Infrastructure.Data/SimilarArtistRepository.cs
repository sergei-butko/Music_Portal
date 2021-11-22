using System.Collections.Generic;
using System.Linq;
using Music_Portal.Domain.Core;
using Music_Portal.Domain.Interfaces;

namespace Music_Portal.Infrastructure.Data
{
    public class SimilarArtistRepository : ISimilarArtistRepository
    {
        private readonly ApplicationContext _db;
        private readonly IArtistRepository _artistRepository;

        public SimilarArtistRepository(ApplicationContext context, IArtistRepository artistRepository)
        {
            _db = context;
            _artistRepository = artistRepository;
        }

        public IEnumerable<Artist> GetSimilarArtists(int artistId)
        {
            return _db.SimilarArtistsToArtist
                .Where(relation => relation.SimilarToId == artistId)
                .Join(_db.Artists,
                    relation => relation.SimilarArtistId,
                    artist => artist.Id,
                    (relation, artist) => artist);
        }
        
        public void CreateRange(IEnumerable<Artist> similarArtists, int baseArtistId)
        {
            foreach (var artist in similarArtists)
            {
                if (_db.Artists.FirstOrDefault(a => a.Name == artist.Name) == null)
                {
                    _artistRepository.Create(artist);
                    _db.SaveChanges();
                }
                Create(artist.Name, baseArtistId);
            }
            _db.SaveChanges();
        }

        private void Create(string similarArtistName, int baseArtistId)
        {
            var similarArtist = _db.Artists.FirstOrDefault(a => a.Name == similarArtistName);
            if (similarArtist != null)
            {
                _db.SimilarArtistsToArtist.Add(new SimilarArtistRelations
                {
                    SimilarToId = baseArtistId,
                    SimilarArtistId = similarArtist.Id
                });
            }
            _db.SaveChanges();
        }
    }
}