using AutoMapper;
using Core;
using Interfaces.Models;

namespace Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ArtistLastFm, Artist>();
        }
    }
}