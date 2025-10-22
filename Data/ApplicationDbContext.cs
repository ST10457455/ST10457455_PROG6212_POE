using ClaimSystem.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimSystem.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ✅ Define your DbSets for each model
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<SupportingDocument> SupportingDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ✅ Ensure SQLite compatibility for string columns
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entity in builder.Model.GetEntityTypes())
                {
                    foreach (var property in entity.GetProperties())
                    {
                        if (property.GetColumnType() != null && property.GetColumnType()!.Contains("nvarchar"))
                        {
                            property.SetColumnType("TEXT");
                        }
                    }
                }
            }

            // ✅ Convert Claim.Status enum to string and set default value
            builder.Entity<Claim>()
                   .Property(c => c.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .HasDefaultValue(ClaimStatus.Pending);

            // ✅ Avoid decimal truncation warnings for currency/decimal values
            builder.Entity<Claim>()
                   .Property(c => c.HourlyRate)
                   .HasPrecision(10, 2);

            builder.Entity<Claim>()
                   .Property(c => c.HoursWorked)
                   .HasPrecision(10, 2);

            // ✅ Configure relationships (optional, but recommended)
            builder.Entity<SupportingDocument>()
                   .HasOne(s => s.Claim)
                   .WithMany(c => c.SupportingDocuments)
                   .HasForeignKey(s => s.ClaimId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
