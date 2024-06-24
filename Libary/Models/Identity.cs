using Microsoft.AspNetCore.Identity;

namespace Libary.Models
{
    public class Identity
    {
        public class User : IdentityUser
        {
            public int Year { get; set; }
        }

    }
}
