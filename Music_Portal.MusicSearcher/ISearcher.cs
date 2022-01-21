namespace Music_Portal.MusicSearcher;

public interface ISearcher
{
    IDictionary<string, string> FindSongs(string folder, string file);
    void SetSongPaths(IDictionary<string, string> songList);
}