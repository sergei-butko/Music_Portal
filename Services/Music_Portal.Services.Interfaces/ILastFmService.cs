using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Interfaces.Models;

namespace Services.Interfaces
{
    public interface ILastFmService
    {
        Task<IEnumerable<ArtistLastFm>> GetTopArtists();
    }
}