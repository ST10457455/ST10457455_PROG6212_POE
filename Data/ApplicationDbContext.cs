using Microsoft.EntityFrameworkCore;
using ClaimSystem.Web.Models;

namespace ClaimSystem.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contractor> Contractors { get; set; } = null!;
        public DbSet<Contract> Contracts { get; set; } = null!;
        public DbSet<MonthlyClaim> Claims { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
