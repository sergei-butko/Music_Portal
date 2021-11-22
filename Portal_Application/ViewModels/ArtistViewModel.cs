using System.Collections.Generic;

namespace Portal_Application.ViewModels
{
    public class ArtistViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Listeners { get; set; }
        public int Playcount { get; set; }
        public string Summary { get; set; }
        public string Biography { get; set; }
        public IEnumerable<TrackViewModel> Tracks { get; set; }
    }
}