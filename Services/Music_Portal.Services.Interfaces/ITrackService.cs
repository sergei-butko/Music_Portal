using System.Threading.Tasks;
using Music_Portal.Domain.Core;
using Music_Portal.Services.Interfaces.Models;
using OneOf;

namespace Music_Portal.Services.Interfaces
{
    public interface ITrackService
    {
        Task<OneOf<Track, InvalidId, TrackNotFound>> GetTrackInfo(int trackId);
        Task<GetTrackResult> GetTrack(int trackId);
    }
}