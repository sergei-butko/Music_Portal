using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Music_Portal.Services.Interfaces;
using Music_Portal.Services.Interfaces.Models;
using Portal_Application.Controllers;
using Xunit;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace Music_Portal.Services.Services.UnitTests.Controllers;

public class TrackControllerTests
{
    private readonly TrackController _target;

    private readonly Mock<ITrackService> _mockTrackService = new Mock<ITrackService>();
    private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

    public TrackControllerTests()
    {
        _target = new TrackController(_mockTrackService.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetTrack_WhenTrackIdExists_ReturnsGetTrackResult()
    {
        // Arrange
        const int trackId = 1;
        var fileName = "Viva la Vida";

        var expectedMemoryStream = new MemoryStream();
        expectedMemoryStream.Position = 0;
        expectedMemoryStream.Write(Encoding.ASCII.GetBytes(fileName) , 0, fileName.Length);

        var getTrackResult = new GetTrackResult(expectedMemoryStream, fileName);

        _mockTrackService.Setup(x => x.GetTrack(trackId))
            .ReturnsAsync(getTrackResult)
            .Verifiable();

        // Act
        var result = await _target.GetTrack(trackId);

        // Assert
        result.FileStream.Should().NotBeNull();
        result.FileStream.Length.Should().Be(expectedMemoryStream.Length);
        result.FileDownloadName.Should().Be(fileName);
    }
}