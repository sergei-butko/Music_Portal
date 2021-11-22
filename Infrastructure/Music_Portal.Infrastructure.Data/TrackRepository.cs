using System.Collections.Generic;
using System.Linq;
using Music_Portal.Domain.Core;
using Music_Portal.Domain.Interfaces;

namespace Music_Portal.Infrastructure.Data
{
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
                _db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var track = _db.Tracks.FirstOrDefault(t => t.Id == id);
            if (track != null)
            {
                _db.Tracks.Remove(track);
            }

            _db.SaveChanges();
        }
    }
}