using Microsoft.AspNetCore.Identity;

namespace Poker.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
