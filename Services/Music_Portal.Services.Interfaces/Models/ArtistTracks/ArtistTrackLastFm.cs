using Music_Portal.Services.Interfaces.Models.ArtistInfo;

namespace Music_Portal.Services.Interfaces.Models.ArtistTracks
{
    public class ArtistTrackLastFm
    {
        public string Name { get; set; }
        public int Playcount { get; set; }
        public int Listeners { get; set; }
        public ArtistLastFm Artist { get; set; }
    }
}