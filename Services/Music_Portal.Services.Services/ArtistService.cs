using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music_Portal.Domain.Core;
using Music_Portal.Domain.Interfaces;
using Music_Portal.Services.Interfaces;
using Music_Portal.Services.Interfaces.Models;
using AutoMapper;
using AutoMapper.Internal;
using OneOf;

namespace Music_Portal.Services.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IMapper _mapper;
        private readonly ILastFmService _lastFmService;
        private readonly IAlbumService _albumService;
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly ISimilarArtistRepository _similarArtistRepository;

        public ArtistService(IMapper mapper, ILastFmService lastFmService, IAlbumService albumService,
            IArtistRepository artistRepository, IAlbumRepository albumRepository,
            ITrackRepository trackRepository, ISimilarArtistRepository similarArtistRepository)
        {
            _mapper = mapper;
            _lastFmService = lastFmService;
            _albumService = albumService;
            _artistRepository = artistRepository;
            _albumRepository = albumRepository;
            _trackRepository = trackRepository;
            _similarArtistRepository = similarArtistRepository;
        }

        public async Task<IEnumerable<Artist>> GetTopArtists()
        {
            var artists = _artistRepository.GetArtists().ToArray();
            if (artists.Any())
            {
                return artists.OrderByDescending(a => a.Listeners);
            }

            var topArtistsLastFm = await _lastFmService.GetTopArtists();
            var topArtists = _mapper.Map<IEnumerable<Artist>>(topArtistsLastFm).ToArray();
            _artistRepository.CreateRange(topArtists);

            return topArtists.OrderByDescending(a => a.Listeners);
        }

        public async Task<OneOf<Artist, InvalidId, ArtistNotFound>> GetArtistInfo(int artistId)
        {
            if (artistId <= 0)
            {
                return new InvalidId();
            }

            var artist = _artistRepository.GetArtistById(artistId);
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

            var mappedSimilarArtists = _mapper.Map<IEnumerable<Artist>>(artistInfoLastFm.Similar.Artist);
            _similarArtistRepository.CreateRange(mappedSimilarArtists, mappedArtist.Id);

            return mappedArtist;
        }

        public async Task<OneOf<IEnumerable<Album>, InvalidId, ArtistNotFound>> GetArtistTopAlbums(int artistId)
        {
            if (artistId <= 0)
            {
                return new InvalidId();
            }

            var artist = _artistRepository.GetArtistById(artistId);
            if (artist == null)
            {
                return new ArtistNotFound();
            }

            var albums = _albumRepository.GetArtistAlbums(artist.Id).ToArray();
            if (albums.Any())
            {
                return albums;
            }

            var topAlbumsLastFm = await _lastFmService.GetArtistTopAlbums(artist.Name);
            var topAlbums = _mapper.Map<IEnumerable<Album>>(
                topAlbumsLastFm.OrderByDescending(a => a.Playcount)).ToArray();
            topAlbums.ForAll(t => t.Artist = artist);
            _albumRepository.CreateRange(topAlbums);

            return topAlbums;
        }

        public async Task<OneOf<IEnumerable<Track>, InvalidId, ArtistNotFound>> GetArtistTopTracks(int artistId)
        {
            if (artistId <= 0)
            {
                return new InvalidId();
            }

            var artist = _artistRepository.GetArtistById(artistId);
            if (artist == null)
            {
                return new ArtistNotFound();
            }

            var tracks = _trackRepository.GetArtistTracks(artist.Id).ToArray();
            if (tracks.Any())
            {
                return tracks;
            }

            var topTracksLastFm = await _lastFmService.GetArtistTopTracks(artist.Name);
            var topTracks = _mapper.Map<IEnumerable<Track>>(topTracksLastFm).ToArray();

            topTracks.ForAll(t => t.Artist = artist);
            _trackRepository.CreateRange(topTracks);

            return topTracks;
        }

        public async Task<OneOf<IEnumerable<Artist>, InvalidId, ArtistNotFound>> GetSimilarArtists(int artistId)
        {
            if (artistId <= 0)
            {
                return new InvalidId();
            }

            var artist = _artistRepository.GetArtistById(artistId);
            if (artist == null)
            {
                return new ArtistNotFound();
            }

            var similarArtists = _similarArtistRepository.GetSimilarArtists(artistId).ToArray();
            if (similarArtists.Any())
            {
                return similarArtists;
            }

            var similarArtistsLastFm = await _lastFmService.GetSimilarArtists(artist.Name);
            var mappedSimilarArtists = _mapper.Map<IEnumerable<Artist>>(similarArtistsLastFm).ToArray();
            _similarArtistRepository.CreateRange(mappedSimilarArtists, artistId);

            return mappedSimilarArtists;
        }
    }
}