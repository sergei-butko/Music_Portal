using System.IO;

namespace Music_Portal.Services.Interfaces.Models
{
    public class GetTrackResult
    {
        public MemoryStream MemoryStream { get; }
        public string ContentType { get; }
        public string FileName { get; set; }

        public GetTrackResult(MemoryStream memoryStream, string fileName)
        {
            MemoryStream = memoryStream;
            ContentType = @"audio/mp3";
            FileName = fileName;
        }
    }
}