using EHT.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EHT.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
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
                //.HasKey(_ => new { _.Id, _.OrganizationId });

            builder.Entity<Business>()
                .HasIndex(_ => new { _.Name, _.CountryId })
                .IsUnique();
                //.HasKey(_ => new { _.Id, _.CountryId });
            //builder.Entity<Business>()
            //    .HasOne(_ => _.Country)
            //    .WithMany(_ => _.Businesses)
            //    .IsRequired()
            //    .HasForeignKey(_ => new { _.CountryId, _.OrganizationId });

            builder.Entity<Family>()
                .HasIndex(_ => new { _.Name, _.BusinessId })
                .IsUnique();
                //.HasKey(_ => new { _.Id, _.BusinessId });
            //builder.Entity<Family>()
            //    .HasOne(_ => _.Business)
            //    .WithMany(_ => _.Families)
            //    .IsRequired()
            //    .HasForeignKey(_ => new { _.BusinessId, _.CountryId });

            builder.Entity<Offering>()
                .HasIndex(_ => new { _.Name, _.FamilyId })
                .IsUnique();
                //.HasKey(_ => new { _.Id, _.FamilyId });
            //builder.Entity<Offering>()
            //    .HasOne(_ => _.Family)
            //    .WithMany(_ => _.Offerings)
            //    .IsRequired()
            //    .HasForeignKey(_ => new { _.FamilyId, _.BusinessId });

            builder.Entity<Department>()
                .HasIndex(_ => new { _.Name, _.OfferingId })
                .IsUnique();
                //.HasKey(_ => new { _.Id, _.OfferingId });
            //builder.Entity<Department>()
            //    .HasOne(_ => _.Offering)
            //    .WithMany(_ => _.Departments)
            //    .IsRequired()
            //    .HasForeignKey(_ => new { _.OfferingId, _.FamilyId });
        }
    }
}
