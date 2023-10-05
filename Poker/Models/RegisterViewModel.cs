using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Poker.Models
{
    public class RegisterViewModel
    {        
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }

        public DateOnly DateRegister { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
