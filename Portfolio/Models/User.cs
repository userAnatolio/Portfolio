using Microsoft.AspNetCore.Identity;

namespace Portfolio.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}