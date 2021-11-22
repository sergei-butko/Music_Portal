using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music_Portal.Domain.Core
{
    public class SimilarArtistRelations
    {
        [Key]
        public int Id { get; set; }
        [Required, ForeignKey("Artist")]
        public int SimilarArtistId { get; set; }
        [Required, ForeignKey("Artist")]
        public int SimilarToId { get; set; }
    }
}