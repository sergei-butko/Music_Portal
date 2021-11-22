using Music_Portal.Domain.Core;
using Music_Portal.Services.Interfaces.Models.AlbumInfo;
using Music_Portal.Services.Interfaces.Models.ArtistAlbums;
using Music_Portal.Services.Interfaces.Models.ArtistInfo;
using Music_Portal.Services.Interfaces.Models.ArtistTracks;
using Music_Portal.Services.Interfaces.Models.TopArtists;
using Music_Portal.Services.Interfaces.Models.TrackInfo;
using AutoMapper;

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
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Bio.Summary))
                .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Bio.Content));
            
            CreateMap<ArtistAlbumLastFm, Album>();
            CreateMap<AlbumLastFm, Album>()
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Wiki.Summary))
                .ForMember(dest => dest.Wiki, opt => opt.MapFrom(src => src.Wiki.Content));
            
            CreateMap<ArtistTrackLastFm, Track>();
            CreateMap<TrackLastFm, Track>()
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Wiki.Summary))
                .ForMember(dest => dest.Wiki, opt => opt.MapFrom(src => src.Wiki.Content));
            CreateMap<TrackArtistLastFm, Artist>();
            CreateMap<TrackAlbumLastFm, Album>();
        }
    }
}