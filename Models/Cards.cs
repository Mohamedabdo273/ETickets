using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ETickets.Models
{
    public class Cards
    {
        
        public int MovieId { get; set; }
        [ValidateNever]
        public Movie Movie { get; set; }
        public string UserId { get; set; }
        [ValidateNever]
        public ApplicationUsers ApplicationUsers { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Count must be 0 or more.")]
        public int count { get; set; }
    }
}
