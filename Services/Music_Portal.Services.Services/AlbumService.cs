using System.Collections.Generic;
using System.Threading.Tasks;
using Music_Portal.Domain.Core;
using Music_Portal.Domain.Interfaces;
using Music_Portal.Services.Interfaces;
using Music_Portal.Services.Interfaces.Models;
using AutoMapper;
using OneOf;

namespace Music_Portal.Services.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly ILastFmService _lastFmService;
        private readonly IMapper _mapper;
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(ILastFmService lastFmService, IMapper mapper, IAlbumRepository albumRepository)
        {
            _lastFmService = lastFmService;
            _mapper = mapper;
            _albumRepository = albumRepository;
        }

        public async Task<OneOf<Album, InvalidId, AlbumNotFound>> GetAlbumInfo(int albumId)
        {
            if (albumId <= 0)
            {
                return new InvalidId();
            }
            var album = _albumRepository.GetAlbum(albumId);
            if (album == null)
            {
                return new AlbumNotFound();
            }

            if (album.Summary != null)
            {
                return album;
            }
            
            var albumInfoLastFm = await _lastFmService.GetAlbumInfo(album.Name, album.Artist.Name);
            var mappedAlbum = _mapper.Map<Album>(albumInfoLastFm);
            mappedAlbum.Id = album.Id;
            mappedAlbum.Name = album.Name;
            _albumRepository.Update(mappedAlbum);
            return mappedAlbum;
        }

        public IEnumerable<Track> GetAlbumTracks(int albumId)
        {
            return _albumRepository.GetAlbum(albumId).Tracks;
        }
    }
}