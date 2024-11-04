using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;


namespace ETickets.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string? CinemaLogo { get; set; }
        public string Address { get; set; }
        [Required]
        [Range(0, 10000)]
        public int? TicketsCount { get; set; }
        [ValidateNever]
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();       
    }
}
