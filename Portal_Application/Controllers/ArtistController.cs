using System;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ApplicationContext _db;
        private readonly ArtistLastFm[] _artists = GetArtists().Result.Artists.Artist.ToArray();

        public ArtistController(ApplicationContext context)
        {
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

        private static async Task<TopArtistsResponseLastFm> GetArtists()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(Environment.GetEnvironmentVariable("URL"));
            var result = await response.Content.ReadAsAsync<TopArtistsResponseLastFm>();

            return result;
        }
        
        [HttpGet]
        public List<Artist> Get()
        {
            return _db.Artists.ToList();
        }
    }
}