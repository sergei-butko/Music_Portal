using System.Collections.Generic;
using System.Linq;
using Music_Portal.Domain.Core;
using Music_Portal.Domain.Interfaces;

namespace Music_Portal.Infrastructure.Data;

public class TrackRepository : ITrackRepository
{
    private readonly ApplicationContext _db;

    public TrackRepository(ApplicationContext context)
    {
        _db = context;
    }

    public IEnumerable<Track> GetArtistTracks(int artistId)
    {
        return _db.Tracks.Where(t => t.Artist.Id == artistId).OrderByDescending(t => t.Listeners);
    }

    public Track GetArtistTrack(int artistId, string trackName)
    {
        return _db.Tracks.Where(t => t.Artist.Id == artistId).FirstOrDefault(t => t.Name == trackName);
    }

    public IEnumerable<Track> GetAlbumTracks(int albumId)
    {
        return _db.Tracks.Where(t => t.Album.Id == albumId);
    }

    public IEnumerable<Track> GetTracks()
    {
        return _db.Tracks;
    }

    public Track GetTrack(int id)
    {
        return _db.Tracks.FirstOrDefault(t => t.Id == id);
    }

    public void Create(Track track)
    {
        _db.Tracks.Add(track);
        _db.SaveChanges();
    }

    public void CreateRange(IEnumerable<Track> tracks)
    {
        _db.Tracks.AddRange(tracks);
        _db.SaveChanges();
    }

    public void Update(Track track)
    {
        var trackToUpdate = _db.Tracks.FirstOrDefault(t => t.Name == track.Name);
        if (trackToUpdate != null)
        {
            trackToUpdate.Playcount = track.Playcount;
            trackToUpdate.Listeners = track.Listeners;
            trackToUpdate.Summary = track.Summary;
            trackToUpdate.Wiki = track.Wiki;
            trackToUpdate.Album = track.Album;
            _db.SaveChanges();
        }
    }

    public void UpdatePath(Track track)
    {
        var trackToUpdate = _db.Tracks.FirstOrDefault(t => t.Name == track.Name);
        if (trackToUpdate != null)
        {
            trackToUpdate.PathToFile = track.PathToFile;
            _db.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var track = _db.Tracks.FirstOrDefault(t => t.Id == id);
        if (track != null)
        {
            _db.Tracks.Remove(track);
            _db.SaveChanges();
        }
    }
}