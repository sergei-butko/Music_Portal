using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Music_Portal.Services.Interfaces.Models;
using Music_Portal.Services.Interfaces;

namespace Music_Portal.Services.Services
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