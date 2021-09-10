using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Interfaces;

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
        public async Task<IEnumerable<Artist>> Get()
        {
            return await _artistService.GetTopArtists();
        }
    }
}