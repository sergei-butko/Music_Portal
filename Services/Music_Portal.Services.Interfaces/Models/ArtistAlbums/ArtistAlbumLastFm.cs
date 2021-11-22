using Music_Portal.Services.Interfaces.Models.ArtistInfo;

namespace Music_Portal.Services.Interfaces.Models.ArtistAlbums
{
    public class ArtistAlbumLastFm
    {
        public string Name { get; set; }
        public int Playcount { get; set; }
        public ArtistLastFm Artist { get; set; }
    }
}