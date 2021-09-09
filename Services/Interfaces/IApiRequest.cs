using System.Threading.Tasks;
using Interfaces.Models;

namespace Interfaces
{
    public interface IApiRequest
    {
        private Task<TopArtistsResponseLastFm> GetArtists()
        {
            throw new System.NotImplementedException();
        }
    }
}