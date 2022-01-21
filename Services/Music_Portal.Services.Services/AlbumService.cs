using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly IMapper _mapper;
        private readonly ILastFmService _lastFmService;
        private readonly IAlbumRepository _albumRepository;
        private readonly ITrackRepository _trackRepository;
        
        public AlbumService(IMapper mapper, ILastFmService lastFmService,
            IAlbumRepository albumRepository, ITrackRepository trackRepository)
        {
            _mapper = mapper;
            _lastFmService = lastFmService;
            _albumRepository = albumRepository;
            _trackRepository = trackRepository;
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

        public async Task<OneOf<IEnumerable<Track>, InvalidId, AlbumNotFound>> GetAlbumTracks(int albumId)
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

            var albumTracks = _trackRepository.GetAlbumTracks(albumId).ToArray();
            if (albumTracks.Any())
            {
                return albumTracks;
            }

            var albumTracksLastFm = await _lastFmService.GetAlbumTracks(album.Name, album.Artist.Name);
            var mappedAlbumTracks = _mapper.Map<IEnumerable<Track>>(albumTracksLastFm).ToArray();
            foreach (var track in mappedAlbumTracks)
            {
                var currentTrack = _trackRepository.GetTracks().FirstOrDefault(t => t.Name == track.Name);
                if (currentTrack != null)
                {
                    track.Id = currentTrack.Id;
                }
                track.Album = album;
                
                _trackRepository.Update(track);
            }
            
            return mappedAlbumTracks;
        }
    }
}