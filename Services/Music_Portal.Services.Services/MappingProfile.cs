using AutoMapper;
using Music_Portal.Domain.Core;
using Music_Portal.Services.Interfaces.Models;

namespace Music_Portal.Services.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ArtistLastFm, Artist>();
        }
    }
}