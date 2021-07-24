using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.AccountModels;
using Blog.Objects;

namespace Portfolio.Data
{
    public class PortfolioContext:IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public PortfolioContext(DbContextOptions<PortfolioContext>options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}
