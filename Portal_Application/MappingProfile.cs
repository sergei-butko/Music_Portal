using Music_Portal.Domain.Core;
using Portal_Application.ViewModels;
using AutoMapper;

namespace Portal_Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Artist, ArtistViewModel>();
            CreateMap<Artist, TopArtistsViewModel>();
            CreateMap<Album, AlbumViewModel>()
                .ForMember(dest => dest.ArtistId, opt => opt.MapFrom(src => src.Artist.Id))
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name));
            CreateMap<Track, TrackViewModel>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.ArtistId, opt => opt.MapFrom(src => src.Artist.Id))
                .ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => src.Album.Name))
                .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.Album.Id));
        }
    }
}