using System.Collections.Generic;
using System.Threading.Tasks;
using Music_Portal.Services.Interfaces.Models.ArtistInfo;
using Music_Portal.Services.Interfaces.Models.TopArtists;
using Music_Portal.Services.Interfaces.Models.ArtistTracks;

namespace Music_Portal.Services.Interfaces
{
    public interface ILastFmService
    {
        Task<IEnumerable<TopArtistLastFm>> GetTopArtists();
        Task<ArtistLastFm> GetArtistInfo(string name);
        Task<IEnumerable<ArtistTrackLastFm>> GetArtistTopTracks(string name);
    }
}