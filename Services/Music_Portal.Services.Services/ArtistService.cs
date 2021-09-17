using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Music_Portal.Domain.Core;
using Music_Portal.Domain.Interfaces;
using Music_Portal.Services.Interfaces;

namespace Music_Portal.Services.Services
{
    public class ArtistService : IArtistService
    {
        private readonly ILastFmService _lastFmService;
        private readonly IMapper _mapper;
        private readonly IArtistRepository _repository;

        public ArtistService(ILastFmService lastFmService, IMapper mapper, IArtistRepository repository)
        {
            _lastFmService = lastFmService;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<Artist>> GetTopArtists()
        {
            var artists = _repository.GetArtistsEnumerable();
            if (artists == null)
            {
                var topArtistsLastFm = await _lastFmService.GetTopArtists();
                var topArtists = _mapper.Map<IEnumerable<Artist>>(topArtistsLastFm);
                _repository.CreateRange(topArtists);
            }

            return artists.OrderByDescending(a => a.Listeners);
        }
    }
}