using Music_Portal.Services.Interfaces.Models.AlbumInfo;
using Music_Portal.Services.Interfaces.Models.TrackInfo;

namespace Music_Portal.Services.Interfaces.Models.ArtistTracks
{
    public class ArtistTrackLastFm
    {
        public string Name { get; set; }
        public int Playcount { get; set; }
        public int Listeners { get; set; }
    }
}