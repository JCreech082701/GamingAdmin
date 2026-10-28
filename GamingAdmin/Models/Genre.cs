using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GamingAdmin.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [ValidateNever]
        public List<Game> Games { get; set; } = new List<Game>();
        public string Title { get; set; } = string.Empty;
    }
}
