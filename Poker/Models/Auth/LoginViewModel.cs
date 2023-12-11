using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Poker.Models.Auth
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
