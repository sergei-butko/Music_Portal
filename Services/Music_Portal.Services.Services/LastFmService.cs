using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Services.Interfaces.Models;
using Services.Interfaces;

namespace Services.Services
{
    public class LastFmService : ILastFmService
    {
        public async Task<IEnumerable<ArtistLastFm>> GetTopArtists()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(Environment.GetEnvironmentVariable("URL"));
            var result = await response.Content.ReadAsAsync<TopArtistsResponseLastFm>();
            return result.Artists.Artist;
        }
    }
}