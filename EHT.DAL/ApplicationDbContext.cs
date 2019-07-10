using EHT.DAL.Entities;
using EHT.DAL.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EHT.DAL
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Offering> Offerings { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Organization>()
                .HasIndex(_ => _.Code)
                .IsUnique();

            builder.Entity<Country>()
                .HasIndex(_ => new { _.Code, _.OrganizationId })
                .IsUnique();

            builder.Entity<Business>()
                .HasIndex(_ => new { _.Name, _.CountryId })
                .IsUnique();

            builder.Entity<Family>()
                .HasIndex(_ => new { _.Name, _.BusinessId })
                .IsUnique();

            builder.Entity<Offering>()
                .HasIndex(_ => new { _.Name, _.FamilyId })
                .IsUnique();

            builder.Entity<Department>()
                .HasIndex(_ => new { _.Name, _.OfferingId })
                .IsUnique();
        }
    }
}
