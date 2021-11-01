using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Music_Portal.Domain.Core;
using Music_Portal.Services.Interfaces;

namespace Portal_Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }
        
        [HttpGet("top_artists")]
        public async Task<IEnumerable<Artist>> GetTopArtists()
        {
            return await _artistService.GetTopArtists();
        }
        
        [HttpGet("artist_info")]
        public async Task<IActionResult> GetArtistInfo(int artistId)
        {
            var artistOneOf = await _artistService.GetArtistInfo(artistId);
            return artistOneOf.Match<IActionResult>(Ok,
                invalidId => BadRequest("Incorrect ID. Must be positive"),
                artistNotFound => NotFound("Artist Not Found"));
        }
        
        [HttpGet("artist_top_tracks")]
        public async Task<IActionResult> GetArtistTopTracks(int artistId)
        {
            var tracksOneOf = await _artistService.GetArtistTopTracks(artistId);
            return tracksOneOf.Match<IActionResult>(Ok,
                invalidId => BadRequest("Incorrect ID. Must be positive"),
                artistNotFound => NotFound("Artist Not Found"));
        }
    }
}