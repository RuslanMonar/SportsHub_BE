using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {

        public DbSet<FooterElement> FooterElements { get; set; }
        public DbSet<FooterArticle> FooterArticles { get; set; }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }


        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}