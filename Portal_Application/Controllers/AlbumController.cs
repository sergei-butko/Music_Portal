using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Music_Portal.Services.Interfaces;
using Portal_Application.ViewModels;
using AutoMapper;
using Music_Portal.Domain.Core;

namespace Portal_Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly IMapper _mapper;

        public AlbumController(IAlbumService albumService, IMapper mapper)
        {
            _albumService = albumService;
            _mapper = mapper;
        }

        [HttpGet("album_info/{albumId}")]
        public async Task<IActionResult> GetAlbumInfo(int albumId)
        {
            var albumOneOf = await _albumService.GetAlbumInfo(albumId);
            return albumOneOf.Match<IActionResult>(
                albumInfo => Ok(_mapper.Map<AlbumViewModel>(albumInfo)),
                invalidId => BadRequest("Incorrect ID. Must be positive"),
                albumNotFound => NotFound("Album Not Found"));
        }

        [HttpGet("album_tracks/{albumId}")]
        public async Task<IActionResult> GetAlbumTracks(int albumId)
        {
            var albumTracksOneOf = await _albumService.GetAlbumTracks(albumId);
            return albumTracksOneOf.Match<IActionResult>(
                albumTracks => Ok(_mapper.Map<IEnumerable<TrackViewModel>>(albumTracks)),
                invalidId => BadRequest("Incorrect ID. Must be positive"),
                albumNotFound => NotFound("Album Not Found"));
        }
    }
}