using Microsoft.EntityFrameworkCore;
using KariyerKocuAPI.Entities;

namespace KariyerKocuAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
    }
}