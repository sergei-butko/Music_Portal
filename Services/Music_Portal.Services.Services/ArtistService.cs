using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Core;
using Infrastructure.Data;
using Services.Interfaces;

namespace Services.Services
{
    public class ArtistService : IArtistService
    {
        private readonly ILastFmService _lastFmService;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _db;

        public ArtistService(ILastFmService lastFmService, IMapper mapper, ApplicationContext db)
        {
            _lastFmService = lastFmService;
            _mapper = mapper;
            _db = db;
        }

        public async Task<IEnumerable<Artist>> GetTopArtists()
        {
            var topArtistsLastFm = await _lastFmService.GetTopArtists();
            var topArtists = _mapper.Map<IEnumerable<Artist>>(topArtistsLastFm);
            
            var repository = new ArtistRepository(_db);
            
            foreach (var artist in topArtists)
            {
                var curArtist = repository.GetArtist(artist.Name);
                if (curArtist != null)
                {
                   repository.Update(curArtist);
                }
                else
                { 
                    repository.Create(new Artist
                    {
                        Name = artist.Name,
                        Url = artist.Url,
                        Playcount = artist.Playcount,
                        Listeners = artist.Listeners
                    });
                }
            }
            return repository.GetArtistsEnumerable().OrderByDescending(a => a.Listeners);
        }
    }
}