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
            if (_repository.GetArtistsEnumerable() == null)
            {
                var topArtistsLastFm = await _lastFmService.GetTopArtists();
                var topArtists = _mapper.Map<IEnumerable<Artist>>(topArtistsLastFm);

                foreach (var artist in topArtists)
                {
                    _repository.Create(new Artist
                    {
                        Name = artist.Name,
                        Url = artist.Url,
                        Playcount = artist.Playcount,
                        Listeners = artist.Listeners
                    });
                }
            }

            return _repository.GetArtistsEnumerable().OrderByDescending(a => a.Listeners);
        }

        public void UpdateArtistsInfo(IEnumerable<Artist> artistsFromRequest)
        {
            foreach (var artist in artistsFromRequest)
            {
                if (_repository.GetArtist(artist.Name) != null)
                {
                    _repository.Update(artist);
                }
                else
                {
                    _repository.Create(artist);
                }
            }
        }
    }
}