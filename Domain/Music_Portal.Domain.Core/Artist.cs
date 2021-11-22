using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Music_Portal.Domain.Core
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public string Name { get; set; }
        public int Listeners { get; set; }
        public int Playcount { get; set; }
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }
        public virtual IEnumerable<Track> Tracks { get; set; } = new List<Track>();
        public virtual IEnumerable<Album> Albums { get; set; } = new List<Album>();
    }
}