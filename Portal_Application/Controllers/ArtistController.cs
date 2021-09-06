using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core;
using Interfaces.Models;

namespace Portal_Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly ILogger<ArtistController> _logger;
        private readonly ApplicationContext _db;

        public ArtistController(ILogger<ArtistController> logger, ApplicationContext context)
        {
            _logger = logger;
            _db = context;
            
            if (!_db.Artists.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    _db.Artists.Add(new Artist
                    {
                        Name = _artists[i].Name,
                        Url = _artists[i].Url,
                        Playcount = _artists[i].Playcount,
                        Listeners = _artists[i].Listeners
                    });
                }

                _db.SaveChanges();
            }
        }

        private static async Task<RootLastFm> GetArtists()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(
                "http://ws.audioscrobbler.com/2.0/?method=chart.gettopartists&" +
                "api_key=b96a38553e5045f118f8097659031ff3&format=json");
            var result = await response.Content.ReadAsAsync<RootLastFm>();

            return result;
        }
        
        readonly ArtistLastFm[] _artists = GetArtists().Result.Artists.Artist.ToArray();
        
        [HttpGet]
        public List<Core.Artist> Get()
        {
            return _db.Artists.ToList();
        }

        [HttpGet("{id}")]
        public Artist Get(int id)
        {
            Artist artist = _db.Artists.FirstOrDefault(x => x.Id == id);
            return artist;
        }
        
        [HttpPost]
        public IActionResult Post(Artist artist)
        {
            if (ModelState.IsValid)
            {
                _db.Artists.Add(artist);
                _db.SaveChanges();
                return Ok(artist);
            }
            return BadRequest(ModelState);
        }
        
        [HttpPut]
        public IActionResult Put(Artist artist)
        {
            if (ModelState.IsValid)
            {
                _db.Update(artist);
                _db.SaveChanges();
                return Ok(artist);
            }
            return BadRequest(ModelState);
        }
 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Artist artist = _db.Artists.FirstOrDefault(x => x.Id == id);
            if (artist != null)
            {
                _db.Artists.Remove(artist);
                _db.SaveChanges();
            }
            return Ok(artist);
        }
    }
}