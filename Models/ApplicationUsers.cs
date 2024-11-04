
using Microsoft.AspNetCore.Identity;

namespace ETickets.Models
{
    public class ApplicationUsers : IdentityUser
    {
        public string? City { get; set; }
       
    }

    
}
