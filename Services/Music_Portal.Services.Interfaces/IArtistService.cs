using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;

namespace Services.Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetTopArtists();
    }
}