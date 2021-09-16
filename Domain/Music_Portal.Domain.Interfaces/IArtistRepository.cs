using System.Collections.Generic;
using Music_Portal.Domain.Core;

namespace Music_Portal.Domain.Interfaces
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> GetArtistsEnumerable();
        Artist GetArtist(string name);
        void Create(Artist artist);
        void Update(Artist artist);
        void Delete(int id);
    }
}