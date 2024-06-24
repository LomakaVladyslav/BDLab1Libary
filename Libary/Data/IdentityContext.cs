using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Libary.Data
{
    public class IdentityContext : IdentityDbContext<User>

    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
           : base(options)
        {
        }

    }
}
