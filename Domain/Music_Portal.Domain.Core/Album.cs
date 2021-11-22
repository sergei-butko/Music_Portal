using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Music_Portal.Domain.Core
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }
        public int Listeners { get; set; }
        public int Playcount { get; set; }
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }
        [DataType(DataType.MultilineText)]
        public string Wiki { get; set; }
        [Required]
        public virtual Artist Artist { get; set; }
        [Required]
        public virtual IEnumerable<Track> Tracks { get; set; } = new List<Track>();
    }
}