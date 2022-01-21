using Music_Portal.Domain.Core;
using Music_Portal.Domain.Interfaces;
using Serilog;

namespace Music_Portal.MusicSearcher;

public class Searcher : ISearcher
{
    private readonly ILogger _logger;
    private readonly IArtistRepository _artistRepository;
    private readonly ITrackRepository _trackRepository;

    private const string Dash = " - ";

    public Searcher(ILogger logger, IArtistRepository artistRepository, ITrackRepository trackRepository)
    {
        _artistRepository = artistRepository;
        _trackRepository = trackRepository;
        _logger = logger;
    }

    public IDictionary<string, string> FindSongs(string folder, string file)
    {
        var songList = new Dictionary<string, string>();
        try
        {
            foreach (string findFile in Directory.EnumerateFiles(
                         folder, file, SearchOption.AllDirectories))
            {
                FileInfo fileInfo = new FileInfo(findFile);
                _logger.Information($"Found file: {fileInfo.Name}");
                songList.Add(fileInfo.Name, fileInfo.FullName);
            }
        }
        catch (UnauthorizedAccessException e)
        {
            _logger.Fatal(e.Message);
            return new Dictionary<string, string>();
        }

        return songList;
    }

    public void SetSongPaths(IDictionary<string, string> songList)
    {
        foreach (var (songName, songPathToFile) in songList)
        {
            var artistName = songName.Substring(0, songName
                .IndexOf(Dash, StringComparison.Ordinal));
            var artist = _artistRepository.GetArtistByName(artistName);

            if (artist == null)
            {
                _logger.Warning($"Artist {artistName} does not exist in the DB yet!");
                return;
            }

            var trackName = songName.Substring(0, songName
                    .IndexOf(Path.GetExtension(songName), StringComparison.Ordinal))
                .Substring(songName.IndexOf(Dash, StringComparison.Ordinal) + Dash.Length);

            UpdateSongFilePath(artist, trackName, artistName, songPathToFile);
        }
    }

    private void UpdateSongFilePath(Artist artist, string trackName, string artistName, string songPathToFile)
    {
        var track = _trackRepository.GetArtistTrack(artist.Id, trackName);

        if (track == null)
        {
            _logger.Warning($"Track {trackName} of artist {artistName} does not exist in the DB yet!");
            return;
        }

        _logger.Information($"DB has track {trackName} of artist {artistName}");
        track.PathToFile = songPathToFile;
        _trackRepository.UpdatePath(track);
        _logger.Information($"Added path for track {trackName} of artist {artistName}");
    }
}