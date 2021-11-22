namespace Portal_Application.ViewModels
{
    public class TrackViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Playcount { get; set; }
        public int Listeners { get; set; }
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string Summary { get; set; }
        public string Wiki { get; set; }
    }
}