using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Music_Portal.Domain.Core;
using Music_Portal.Domain.Interfaces;
using Music_Portal.Services.Interfaces;
using Music_Portal.Services.Interfaces.Models;
using AutoMapper;
using OneOf;

namespace Music_Portal.Services.Services
{
    public class TrackService : ITrackService
    {
        private readonly ILastFmService _lastFmService;
        private readonly IMapper _mapper;
        private readonly ITrackRepository _trackRepository;

        public TrackService(ILastFmService lastFmService, IMapper mapper, ITrackRepository trackRepository)
        {
            _lastFmService = lastFmService;
            _mapper = mapper;
            _trackRepository = trackRepository;
        }

        public async Task<OneOf<Track, InvalidId, TrackNotFound>> GetTrackInfo(int trackId)
        {
            if (trackId <= 0)
            {
                return new InvalidId();
            }

            var track = _trackRepository.GetTrack(trackId);
            if (track == null)
            {
                return new TrackNotFound();
            }

            if (track.Wiki != null)
            {
                return track;
            }

            var trackInfoLastFm = await _lastFmService.GetTrackInfo(track.Name, track.Artist.Name);
            var mappedTrack = _mapper.Map<Track>(trackInfoLastFm);
            mappedTrack.Id = track.Id;
            _trackRepository.Update(mappedTrack);
            return mappedTrack;
        }

        public async Task<GetTrackResult> GetTrack(int trackId)
        {
            var filePath = _trackRepository.GetTrack(trackId).PathToFile;
            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            var fileName = Path.GetFileName(filePath);
            return new GetTrackResult(memory, fileName);
        }
    }
}