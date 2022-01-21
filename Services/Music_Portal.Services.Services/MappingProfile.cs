using Music_Portal.Domain.Core;
using Music_Portal.Services.Interfaces.Models.AlbumInfo;
using Music_Portal.Services.Interfaces.Models.ArtistAlbums;
using Music_Portal.Services.Interfaces.Models.ArtistInfo;
using Music_Portal.Services.Interfaces.Models.ArtistTracks;
using Music_Portal.Services.Interfaces.Models.SimilarArtists;
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
            CreateMap<SimilarArtistLastFm, Artist>();

            CreateMap<ArtistAlbumLastFm, Album>();
            CreateMap<AlbumLastFm, Album>()
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Wiki.Summary))
                .ForMember(dest => dest.Wiki, opt => opt.MapFrom(src => src.Wiki.Content))
                .ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.Tracks.Track));
            CreateMap<AlbumTrackLastFm, Track>();

            CreateMap<ArtistTrackLastFm, Track>();
            CreateMap<TrackLastFm, Track>()
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Wiki.Summary))
                .ForMember(dest => dest.Wiki, opt => opt.MapFrom(src => src.Wiki.Content));
            CreateMap<AlbumLastFm, TrackAlbumLastFm>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name));
        }
    }
}