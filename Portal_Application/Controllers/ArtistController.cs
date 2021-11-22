using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Music_Portal.Services.Interfaces;
using Portal_Application.ViewModels;
using AutoMapper;

namespace Portal_Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;


        public ArtistController(IArtistService artistService, IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }

        [HttpGet("top_artists")]
        public async Task<IEnumerable<TopArtistsViewModel>> GetTopArtists()
        {
            var artists = await _artistService.GetTopArtists();
            var topArtists = _mapper.Map<IEnumerable<TopArtistsViewModel>>(artists);
            return topArtists;
        }

        [HttpGet("artist_info/{artistId}")]
        public async Task<IActionResult> GetArtistInfo(int artistId)
        {
            var artistOneOf = await _artistService.GetArtistInfo(artistId);
            return artistOneOf.Match<IActionResult>(
                artistInfo => Ok(_mapper.Map<ArtistViewModel>(artistInfo)),
                invalidId => BadRequest("Incorrect ID. Must be positive"),
                artistNotFound => NotFound("Artist Not Found"));
        }
        
        [HttpGet("artist_top_albums/{artistId}")]
        public async Task<IActionResult> GetArtistTopAlbums(int artistId)
        {
            var albumOneOf = await _artistService.GetArtistTopAlbums(artistId);
            return albumOneOf.Match<IActionResult>(
                albums => Ok(_mapper.Map<IEnumerable<AlbumViewModel>>(albums)),
                invalidId => BadRequest("Incorrect ID. Must be positive"),
                albumNotFound => NotFound("Album Not Found"));
        }

        [HttpGet("artist_top_tracks/{artistId}")]
        public async Task<IActionResult> GetArtistTopTracks(int artistId)
        {
            var tracksOneOf = await _artistService.GetArtistTopTracks(artistId);
            return tracksOneOf.Match<IActionResult>(
                tracks => Ok(_mapper.Map<IEnumerable<TrackViewModel>>(tracks)),
                invalidId => BadRequest("Incorrect ID. Must be positive"),
                artistNotFound => NotFound("Artist Not Found"));
        }

        [HttpGet("similar_artists/{artistId}")]
        public async Task<IActionResult> GetSimilarArtists(int artistId)
        {
            var artistOneOf = await _artistService.GetSimilarArtists(artistId);
            return artistOneOf.Match<IActionResult>(
                similarArtists => Ok(_mapper.Map<IEnumerable<ArtistViewModel>>(similarArtists)),
                invalidId => BadRequest("Incorrect ID. Must be positive"),
                artistNotFound => NotFound("Artist Not Found"));
        }
    }
}