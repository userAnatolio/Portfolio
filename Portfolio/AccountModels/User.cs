using Microsoft.AspNetCore.Identity;

namespace Portfolio.AccountModels
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}