using System.Collections.Generic;

namespace Music_Portal.Services.Interfaces.Models.ArtistInfo
{
    public class SimilarArtistsLastFm
    {
        public IEnumerable<ArtistLastFm> Artist { get; set; }
    }
}