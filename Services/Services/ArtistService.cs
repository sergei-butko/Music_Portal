using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core;
using Interfaces;

namespace Services
{
    public class ArtistService : IArtistService
    {
        private readonly ILastFmService _lastFmService;
        private readonly IMapper _mapper;

        public ArtistService(ILastFmService lastFmService,IMapper mapper)
        {
            _lastFmService = lastFmService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Artist>> GetTopArtists()
        {
            var topArtists = await _lastFmService.GetTopArtists();
            return _mapper.Map<IEnumerable<Artist>>(topArtists);
        }
    }
}