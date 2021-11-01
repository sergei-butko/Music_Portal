using Music_Portal.Services.Interfaces.Models.ArtistInfo;

namespace Music_Portal.Services.Interfaces.Models.ArtistTracks
{
    public class ArtistTrackLastFm
    {
        public string Name { get; set; }
        public string Playcount { get; set; }
        public string Listeners { get; set; }
        public string Url { get; set; }
        public ArtistLastFm Artist { get; set; }
    }
}