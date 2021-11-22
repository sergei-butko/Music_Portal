using System.Collections.Generic;
using System.Linq;
using Music_Portal.Domain.Core;
using Music_Portal.Domain.Interfaces;

namespace Music_Portal.Infrastructure.Data
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ApplicationContext _db;

        public AlbumRepository(ApplicationContext context)
        {
            _db = context;
        }

        public IEnumerable<Album> GetArtistAlbums(int artistId)
        {
            return _db.Albums.Where(a => a.Artist.Id == artistId).OrderByDescending(a => a.Listeners);
        }

        public Album GetAlbum(int id)
        {
            return _db.Albums.FirstOrDefault(a => a.Id == id);
        }

        public void Create(Album album)
        {
            _db.Albums.Add(album);
            _db.SaveChanges();
        }
        
        public void CreateRange(IEnumerable<Album> albums)
        {
            _db.Albums.AddRange(albums);
            _db.SaveChanges();
        }

        public void Update(Album album)
        {
            var albumToUpdate = _db.Albums.FirstOrDefault(a => a.Name == album.Name);
            if (albumToUpdate != null)
            {
                albumToUpdate.Playcount = album.Playcount;
                albumToUpdate.Listeners = album.Listeners;
                albumToUpdate.Summary = album.Summary;
                albumToUpdate.Wiki = album.Wiki;
                _db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var album = _db.Albums.FirstOrDefault(a => a.Id == id);
            if (album != null)
            {
                _db.Albums.Remove(album);
            }

            _db.SaveChanges();
        }
    }
}