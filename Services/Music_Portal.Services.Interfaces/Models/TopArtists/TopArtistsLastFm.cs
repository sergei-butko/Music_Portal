using System.Collections.Generic;

namespace Music_Portal.Services.Interfaces.Models.TopArtists
{
    public class TopArtistsLastFm
    {
        public IEnumerable<TopArtistLastFm> Artist { get; set; }
    }
}