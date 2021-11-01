using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using Music_Portal.Domain.Core;
using Music_Portal.Domain.Interfaces;
using Music_Portal.Services.Interfaces;
using Music_Portal.Services.Interfaces.Models;
using OneOf;

namespace Music_Portal.Services.Services
{
    public class ArtistService : IArtistService
    {
        private readonly ILastFmService _lastFmService;
        private readonly IMapper _mapper;
        private readonly IArtistRepository _artistRepository;
        private readonly ITrackRepository _trackRepository;

        public ArtistService(ILastFmService lastFmService, IMapper mapper, 
            IArtistRepository artistRepository, ITrackRepository trackRepository)
        {
            _lastFmService = lastFmService;
            _mapper = mapper;
            _artistRepository = artistRepository;
            _trackRepository = trackRepository;
        }

        public async Task<IEnumerable<Artist>> GetTopArtists()
        {
            var artists = _artistRepository.GetArtists().ToList();
            if (artists.Count > 0)
            {
                return artists.OrderByDescending(a => a.Listeners);
            }
            
            var topArtistsLastFm = await _lastFmService.GetTopArtists();
            var topArtists = _mapper.Map<IEnumerable<Artist>>(topArtistsLastFm).ToList();
            _artistRepository.CreateRange(topArtists);
            
            return topArtists.OrderByDescending(a => a.Listeners);
        }

        public async Task<OneOf<Artist, InvalidId, ArtistNotFound>> GetArtistInfo(int artistId)
        {
            if (artistId <= 0)
            {
                return new InvalidId();
            }
            var artist = _artistRepository.GetArtist(artistId);
            if (artist == null)
            {
                return new ArtistNotFound();
            }
            if (artist.Biography != null)
            {
                return artist;
            }
            
            var artistInfoLastFm = await _lastFmService.GetArtistInfo(artist.Name);
            var mappedArtist = _mapper.Map<Artist>(artistInfoLastFm);
            mappedArtist.Id = artist.Id;
            _artistRepository.Update(mappedArtist);
            return mappedArtist;
        }
        
        public async Task<OneOf<IEnumerable<Track>, InvalidId, ArtistNotFound>> GetArtistTopTracks(int artistId)
        {
            if (artistId <= 0)
            {
                return new InvalidId();
            }
            var artist = _artistRepository.GetArtist(artistId);
            if (artist == null)
            {
                return new ArtistNotFound();
            }
            var tracks = _trackRepository.GetArtistTracks(artist.Id).ToList();
            if (tracks.Count > 0)
            {
                return tracks;
            }
            
            var topTracksLastFm = await _lastFmService.GetArtistTopTracks(artist.Name);
            var topTracks = _mapper.Map<IEnumerable<Track>>(topTracksLastFm).ToArray();
            topTracks.ForAll(t => t.Artist = artist);
            _trackRepository.CreateRange(topTracks);
            
            return topTracks;
        }
    }
}