using System.Collections.Generic;
using System.Threading.Tasks;
using Music_Portal.Domain.Core;

namespace Music_Portal.Services.Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetTopArtists();
    }
}