using System.Collections.Generic;

namespace Music_Portal.Services.Interfaces.Models.AlbumInfo
{
    public class AlbumLastFm
    {
        public string Name { get; set; }
        public int Listeners { get; set; }
        public int Playcount { get; set; }
        public IEnumerable<AlbumTrackLastFm> Track { get; set; }
        public AlbumWikiLastFm Wiki { get; set; }
    }
}