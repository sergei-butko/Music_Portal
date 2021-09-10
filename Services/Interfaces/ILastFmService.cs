using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Models;

namespace Interfaces
{
    public interface ILastFmService
    {
        Task<IEnumerable<ArtistLastFm>> GetTopArtists();
    }
}