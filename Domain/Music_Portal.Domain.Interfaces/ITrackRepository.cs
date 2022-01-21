using System.Collections.Generic;
using Music_Portal.Domain.Core;

namespace Music_Portal.Domain.Interfaces;

public interface ITrackRepository
{
    IEnumerable<Track> GetArtistTracks(int artistId);
    Track GetArtistTrack(int artistId, string trackName);
    IEnumerable<Track> GetAlbumTracks(int albumId);
    IEnumerable<Track> GetTracks();
    Track GetTrack(int id);
    void Create(Track track);
    void CreateRange(IEnumerable<Track> tracks);
    void Update(Track track);
    void UpdatePath(Track track);
    void Delete(int id);
}