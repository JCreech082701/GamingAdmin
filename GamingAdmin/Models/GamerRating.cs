using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GamingAdmin.Models
{
    public class GamerRating
    {
        public int Id { get; set; }

        [ValidateNever]
        [ScaffoldColumn(false)]
        public List<Game> Games { get; set; } = new List<Game>();

        [Required]
        [StringLength(10)]
        public string Title { get; set; } = string.Empty;   // E, E10+, T, M Following ESRB standards
    }
}

