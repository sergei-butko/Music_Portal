using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Interfaces;
using Interfaces.Models;

namespace Services
{
    public class ApiRequest : IApiRequest
    {
        private readonly List<ArtistLastFm> _artists = GetArtists()
            .Result.Artists.Artist.OrderByDescending(a => a.Listeners).ToList();

        public List<ArtistLastFm> GetTopArtists() => _artists;

        private static async Task<TopArtistsResponseLastFm> GetArtists()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(Environment.GetEnvironmentVariable("URL"));
            var result = await response.Content.ReadAsAsync<TopArtistsResponseLastFm>();

            return result;
        }
    }
}