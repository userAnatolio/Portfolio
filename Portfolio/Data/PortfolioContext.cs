using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Data
{
    public class PortfolioContext:IdentityDbContext
    {
        public PortfolioContext(DbContextOptions<PortfolioContext>options):base(options)
        {

        }
    }
}
