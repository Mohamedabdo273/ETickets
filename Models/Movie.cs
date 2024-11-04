using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ETickets.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [ValidateNever]
        public string? ImgUrl { get; set; }
        [ValidateNever]
        public string? TrailerUrl { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public MovieStatus MovieStatus { get; set; }
        [Required]
        public int CinemaId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Cinema Cinema { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public ICollection<ActorMovie> Actors { get; set; } = new List<ActorMovie>();
        [Range(0, int.MaxValue, ErrorMessage = "Count must be 0 or more.")]
        public int quantity { get; set; }

    }
}
