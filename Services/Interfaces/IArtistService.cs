using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetArtists();
    }
}