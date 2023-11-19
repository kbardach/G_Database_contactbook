using Microsoft.EntityFrameworkCore;
using ContactBook_Database.Models.Entities;

namespace Kontaktbok_Databas.Contexts
{
    internal class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<ContactInformationEntity> ContactInformations { get; set; }
        public DbSet<SocialMediaPlatformEntity> SocialMediaPlatforms { get; set; }
        public DbSet<SocialMediaUserNameEntity> SocialMediaUserNames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SocialMediaUserNameEntity>()
                .HasKey(smun => new { smun.ContactId, smun.SocialMediaPlatformId });
        }
    }
}
