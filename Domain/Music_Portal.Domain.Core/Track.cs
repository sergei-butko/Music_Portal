using System.ComponentModel.DataAnnotations;

namespace Music_Portal.Domain.Core
{
    public class Track
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }
        public int Playcount { get; set; }
        public int Listeners { get; set; }
        [MaxLength(200)]
        public string Url { get; set; }
        [Required]
        public virtual Artist Artist { get; set; }
    }
}