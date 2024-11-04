using System.ComponentModel.DataAnnotations;

namespace ETickets.ViewModel
{
    public class loginVm
    {
        public int Id { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
