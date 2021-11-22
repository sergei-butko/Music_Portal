namespace Music_Portal.Services.Interfaces.Models.ArtistInfo
{
    public class ArtistLastFm
    {
        public string Name { get; set; }
        public ArtistStatsLastFm Stats { get; set; }
        public SimilarArtistsLastFm Similar { get; set; }
        public ArtistBioLastFm Bio { get; set; }
    }
}