using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        DbSet<Team> Teams { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}