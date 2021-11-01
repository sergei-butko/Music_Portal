using System.Collections.Generic;
using System.Threading.Tasks;
using Music_Portal.Domain.Core;
using Music_Portal.Services.Interfaces.Models;
using OneOf;

namespace Music_Portal.Services.Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetTopArtists();
        Task<OneOf<Artist, InvalidId, ArtistNotFound >> GetArtistInfo(int id);
        Task<OneOf<IEnumerable<Track>, InvalidId, ArtistNotFound >>GetArtistTopTracks(int id);
    }
}