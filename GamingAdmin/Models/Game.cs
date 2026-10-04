using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GamingAdmin.Models
{
    public class Game
    {
        public int Id { get; set; }

        // Data Annotations for validation and display purposes
        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Synopsis is required")]
        [Display(Name = "Synopsis")]
        public string Synopsis { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required")]
        [Display(Name = "Genre")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Platform is required")]
        [Display(Name = "Platform")]
        public string Platform { get; set; } = string.Empty;

        [Required(ErrorMessage = "Rating is required")]
        [Display(Name = "Rating")]
        public string Rating { get; set; } = string.Empty;

        [Required(ErrorMessage = "Achievements are required")]
        [Display(Name = "Achievements")]
        public string Achievements { get; set; } = string.Empty;

        [Required(ErrorMessage = "Release Date is required")]
        [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000.00, ErrorMessage = "Price must be between $0.01 and $1000.00")]
        public decimal Price { get; set; }

    } // End of class Game
} // End of namespace GamingAdmin.Models
