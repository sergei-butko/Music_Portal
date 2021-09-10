using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Interfaces;
using Interfaces.Models;

namespace Services
{
    public class LastFmService : ILastFmService
    {
        public async Task<IEnumerable<ArtistLastFm>> GetTopArtists()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(Environment.GetEnvironmentVariable("URL"));
            var result = await response.Content.ReadAsAsync<TopArtistsResponseLastFm>();
            return result.Artists.Artist.OrderByDescending(a => a.Listeners);
        }
    }
}