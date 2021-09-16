using System.Collections.Generic;
using System.Threading.Tasks;
using Music_Portal.Services.Interfaces.Models;

namespace Music_Portal.Services.Interfaces
{
    public interface ILastFmService
    {
        Task<IEnumerable<ArtistLastFm>> GetTopArtists();
    }
}