using System.Collections.Generic;
using Music_Portal.Domain.Core;

namespace Music_Portal.Domain.Interfaces
{
    public interface ISimilarArtistRepository
    {
        IEnumerable<Artist> GetSimilarArtists(int artistId);
        void CreateRange(IEnumerable<Artist> artists, int baseArtistId);
    }
}