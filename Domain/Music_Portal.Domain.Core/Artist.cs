namespace Music_Portal.Domain.Core
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Playcount { get; set; }
        public int Listeners { get; set; }
        public string Url { get; set; }
    }
}