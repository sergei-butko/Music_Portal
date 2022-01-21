using System;
using System.IO;
using System.Threading.Tasks;
using Music_Portal.Domain.Core;
using Music_Portal.Domain.Interfaces;
using Music_Portal.Services.Interfaces;
using AutoMapper;
using Xunit;
using Moq;
using FluentAssertions;

namespace Music_Portal.Services.Services.UnitTests.Services
{
    public class TrackServiceTests
    {
        private readonly TrackService _target;

        private readonly Mock<ITrackRepository> _mockTrackRepository = new Mock<ITrackRepository>();
        private readonly Mock<ILastFmService> _mockLastFmService = new Mock<ILastFmService>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        public TrackServiceTests()
        {
            _target = new TrackService(_mockLastFmService.Object, _mockMapper.Object, _mockTrackRepository.Object);
        }

        [Fact]
        public async Task GetTrack_WhenTrackPathExists_ReturnsGetTrackResult()
        {
            // Arrange
            const string fileName = "test.mp3";
            const string filePath = $"TestFiles/{fileName}";

            var track = new Track {Id = 1};
            _mockTrackRepository.Setup(x => x.GetTrack(track.Id))
                .Returns(new Track {PathToFile = filePath})
                .Verifiable();

            // Act
            var result = await _target.GetTrack(track.Id);

            // Assert
            result.FileName.Should().Be(fileName);
            result.MemoryStream.Length.Should().Be(new FileInfo(filePath).Length);
        }

        [Fact]
        public async Task GetTrack_WhenNoTrackPathExists_ThrowException()
        {
            // Arrange
            var track = new Track {Id = 1};
            _mockTrackRepository.Setup(x => x.GetTrack(track.Id))
                .Returns(new Track {PathToFile = null})
                .Verifiable();

            // Act
            Func<Task> result = async () => { await _target.GetTrack(track.Id); };

            // Assert
            await result.Should().ThrowAsync<ArgumentException>();
        }
    }
}