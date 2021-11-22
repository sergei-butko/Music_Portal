using System.Collections.Generic;
using Music_Portal.Domain.Core;

namespace Music_Portal.Domain.Interfaces
{
    public interface IAlbumRepository
    {
        IEnumerable<Album> GetArtistAlbums(int artistId);
        Album GetAlbum(int id);
        void Create(Album album);
        void CreateRange(IEnumerable<Album> albums);
        void Update(Album album);
        void Delete(int id);
    }
}