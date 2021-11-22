using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Music_Portal.Services.Interfaces;
using Music_Portal.Services.Interfaces.Models.AlbumInfo;
using Music_Portal.Services.Interfaces.Models.ArtistAlbums;
using Music_Portal.Services.Interfaces.Models.ArtistInfo;
using Music_Portal.Services.Interfaces.Models.ArtistTracks;
using Music_Portal.Services.Interfaces.Models.TopArtists;
using Music_Portal.Services.Interfaces.Models.TrackInfo;

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
            result.Artist.Bio.Summary = TrimRedundantText(result.Artist.Bio.Summary);
            result.Artist.Bio.Content = TrimRedundantText(result.Artist.Bio.Content);
            return result.Artist;
        }

        public async Task<IEnumerable<ArtistAlbumLastFm>> GetArtistTopAlbums(string name)
        {
            var artistName = TrimAndReplaceSpacesToPluses(name);
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(
                $"{Environment.GetEnvironmentVariable("REQUEST")}?method=artist.gettopalbums&artist={artistName}" +
                $"&api_key={Environment.GetEnvironmentVariable("API_KEY")}&format=json");
            var result = await response.Content.ReadAsAsync<ArtistAlbumsResponseLastFm>();
            return result.TopAlbums.Album.OrderByDescending(a => a.Playcount);
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

        public async Task<IEnumerable<ArtistLastFm>> GetSimilarArtists(string name)
        {
            var artistName = TrimAndReplaceSpacesToPluses(name);
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(
                $"{Environment.GetEnvironmentVariable("REQUEST")}?method=artist.getinfo&artist={artistName}" +
                $"&api_key={Environment.GetEnvironmentVariable("API_KEY")}&format=json");
            var result = await response.Content.ReadAsAsync<ArtistInfoResponseLastFm>();
            return result.Artist.Similar.Artist;
        }

        public async Task<TrackLastFm> GetTrackInfo(string track, string artist)
        {
            var trackName = TrimAndReplaceSpacesToPluses(track);
            var artistName = TrimAndReplaceSpacesToPluses(artist);
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(
                $"{Environment.GetEnvironmentVariable("REQUEST")}?method=track.getInfo&api_key=" +
                $"{Environment.GetEnvironmentVariable("API_KEY")}&artist={artistName}&track={trackName}&format=json");
            var result = await response.Content.ReadAsAsync<TrackResponseLastFm>();
            result.Track.Wiki.Summary = TrimRedundantText(result.Track.Wiki.Summary);
            result.Track.Wiki.Content = TrimRedundantText(result.Track.Wiki.Content);
            return result.Track;
        }

        public async Task<AlbumLastFm> GetAlbumInfo(string album, string artist)
        {
            var albumName = TrimAndReplaceSpacesToPluses(album);
            var artistName = TrimAndReplaceSpacesToPluses(artist);
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(
                $"{Environment.GetEnvironmentVariable("REQUEST")}?method=album.getinfo&api_key=" +
                $"{Environment.GetEnvironmentVariable("API_KEY")}&artist={artistName}&album={albumName}&format=json");
            var result = await response.Content.ReadAsAsync<AlbumResponseLastFm>();
            result.Album.Name = album;
            if (result.Album.Wiki != null)
            {
                result.Album.Wiki.Summary = TrimRedundantText(result.Album.Wiki.Summary);
                result.Album.Wiki.Content = TrimRedundantText(result.Album.Wiki.Content);
            }

            return result.Album;
        }

        private string TrimAndReplaceSpacesToPluses(string name)
        {
            return name.Trim().Replace(' ', '+');
        }

        private string TrimRedundantText(string wiki)
        {
            return wiki.Substring(0, wiki.IndexOf("<a href"));
        }
    }
}