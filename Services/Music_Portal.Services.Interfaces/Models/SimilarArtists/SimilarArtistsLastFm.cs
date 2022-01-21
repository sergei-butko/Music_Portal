using System.Collections.Generic;

namespace Music_Portal.Services.Interfaces.Models.SimilarArtists
{
    public class SimilarArtistsLastFm
    {
        public IEnumerable<SimilarArtistLastFm> Artist { get; set; }
    }
}