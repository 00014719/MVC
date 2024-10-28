using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required, MinLength(3)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required, Range(0, 5)]
        public float Rating { get; set; }

        public int ReleaseYear { get; set; }

        public string Director { get; set; } = string.Empty;
    }
}
