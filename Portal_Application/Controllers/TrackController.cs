using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Music_Portal.Services.Interfaces;
using Portal_Application.ViewModels;
using AutoMapper;

namespace Portal_Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITrackService _trackService;

        public TrackController(ITrackService trackService, IMapper mapper)
        {
            _trackService = trackService;
            _mapper = mapper;
        }

        [HttpGet("track_info/{trackId}")]
        public async Task<IActionResult> GetTrackInfo(int trackId)
        {
            var trackOneOf = await _trackService.GetTrackInfo(trackId);
            return trackOneOf.Match<IActionResult>(
                trackInfo => Ok(_mapper.Map<TrackViewModel>(trackInfo)),
                invalidId => BadRequest("Incorrect ID. Must be positive"),
                trackNotFound => NotFound("Track Not Found"));
        }

        [HttpGet("get_track/{trackId}")]
        public async Task<FileStreamResult> GetTrack(int trackId)
        {
            var getTrackResult = await _trackService.GetTrack(trackId);

            return File(getTrackResult.MemoryStream, getTrackResult.ContentType, getTrackResult.FileName);
        }
    }
}