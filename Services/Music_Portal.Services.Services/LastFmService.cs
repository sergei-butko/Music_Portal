using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Music_Portal.Services.Interfaces;
using Music_Portal.Services.Interfaces.Models.ArtistInfo;
using Music_Portal.Services.Interfaces.Models.ArtistTracks;
using Music_Portal.Services.Interfaces.Models.TopArtists;

namespace Music_Portal.Services.Services
{
    public class LastFmService : ILastFmService
    {
        public async Task<IEnumerable<TopArtistLastFm>> GetTopArtists()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(
                $"{Environment.GetEnvironmentVariable("REQUEST")}?method=chart.gettopartists" +
                $"&api_key={Environment.GetEnvironmentVariable("API_KEY")}&format=json");
            var result = await response.Content.ReadAsAsync<TopArtistsResponseLastFm>();
            return result.Artists.Artist;
        }
        
        public async Task<ArtistLastFm> GetArtistInfo(string name)
        {
            var artistName = TrimAndReplaceSpacesToPluses(name);
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(
                $"{Environment.GetEnvironmentVariable("REQUEST")}?method=artist.getinfo&artist={artistName}" +
                $"&api_key={Environment.GetEnvironmentVariable("API_KEY")}&format=json");
            var result = await response.Content.ReadAsAsync<ArtistInfoResponseLastFm>();
            return result.Artist;
        }
        
        public async Task<IEnumerable<ArtistTrackLastFm>> GetArtistTopTracks(string name)
        {
            var artistName = TrimAndReplaceSpacesToPluses(name);
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(
                $"{Environment.GetEnvironmentVariable("REQUEST")}?method=artist.gettoptracks&artist={artistName}" +
                $"&api_key={Environment.GetEnvironmentVariable("API_KEY")}&format=json");
            var result = await response.Content.ReadAsAsync<ArtistTracksResponseLastFm>();
            return result.TopTracks.Track.OrderByDescending(t => t.Listeners);
        }

        private string TrimAndReplaceSpacesToPluses(string name)
        {
            return name.Trim().Replace(' ', '+');
        }
    }
}