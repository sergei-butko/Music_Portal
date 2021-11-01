using AutoMapper;
using Music_Portal.Domain.Core;
using Music_Portal.Services.Interfaces.Models.ArtistInfo;
using Music_Portal.Services.Interfaces.Models.TopArtists;
using Music_Portal.Services.Interfaces.Models.ArtistTracks;

namespace Music_Portal.Services.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TopArtistLastFm, Artist>();
            CreateMap<ArtistLastFm, Artist>()
                .ForMember(dest => dest.Listeners, opt => opt.MapFrom(src => src.Stats.Listeners))
                .ForMember(dest => dest.Playcount, opt => opt.MapFrom(src => src.Stats.Playcount))
                .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Bio.Content));
            CreateMap<ArtistTrackLastFm, Track>()
                .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist));
        }
    }
}