using Microsoft.AspNetCore.Identity;

namespace Libary.Data
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }

}
