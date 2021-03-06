using System.Collections.Generic;
using Music_Portal.Domain.Core;

namespace Music_Portal.Domain.Interfaces
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> GetArtists();
        Artist GetArtistById(int id);
        Artist GetArtistByName(string name);
        void Create(Artist artist);
        void CreateRange(IEnumerable<Artist> artists);
        void Update(Artist artist);
        void Delete(int id);
    }
}