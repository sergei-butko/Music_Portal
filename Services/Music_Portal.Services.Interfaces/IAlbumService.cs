using System.Collections.Generic;
using System.Threading.Tasks;
using Music_Portal.Domain.Core;
using Music_Portal.Services.Interfaces.Models;
using OneOf;

namespace Music_Portal.Services.Interfaces
{
    public interface IAlbumService
    {
        Task<OneOf<Album, InvalidId, AlbumNotFound >> GetAlbumInfo(int albumId);
        IEnumerable<Track> GetAlbumTracks(int albumId);
    }
}