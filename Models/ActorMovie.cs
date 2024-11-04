using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ETickets.Models
{
    public class ActorMovie
    {
        public int Id { get; set; }
        public int ActorId { get; set; }
        [ValidateNever]
        public Actor? Actor { get; set; }
        public int MovieId { get; set; }
        [ValidateNever]
        public Movie? Movie { get; set; }

    }
}
