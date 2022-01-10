using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class CreateFilmDto
    {
        public string Title { get; set; }

        [Required(ErrorMessage = "Director is required")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(35, ErrorMessage = "Category must have a maximum of 60 characters")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(30, 600, ErrorMessage = "Duration must have a minimum 30 and maximum 600")]
        public int Duration { get; set; }
    }
}
