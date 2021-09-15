using AutoMapper;
using Domain.Core;
using Services.Interfaces.Models;

namespace Services.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ArtistLastFm, Artist>();
        }
    }
}