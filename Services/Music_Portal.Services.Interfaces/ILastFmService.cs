using System.Collections.Generic;
using System.Threading.Tasks;
using Music_Portal.Services.Interfaces.Models.AlbumInfo;
using Music_Portal.Services.Interfaces.Models.ArtistAlbums;
using Music_Portal.Services.Interfaces.Models.ArtistInfo;
using Music_Portal.Services.Interfaces.Models.TopArtists;
using Music_Portal.Services.Interfaces.Models.ArtistTracks;
using Music_Portal.Services.Interfaces.Models.TrackInfo;

namespace Music_Portal.Services.Interfaces
{
    public interface ILastFmService
    {
        Task<IEnumerable<TopArtistLastFm>> GetTopArtists();
        Task<ArtistLastFm> GetArtistInfo(string name);
        Task<IEnumerable<ArtistAlbumLastFm>> GetArtistTopAlbums(string name);
        Task<AlbumLastFm> GetAlbumInfo(string album, string artist);
        Task<IEnumerable<ArtistTrackLastFm>> GetArtistTopTracks(string name);
        Task<TrackLastFm> GetTrackInfo(string track, string artist);
        Task<IEnumerable<ArtistLastFm>> GetSimilarArtists(string name);
    }
}