using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Equinox.Models.DomainModels;

namespace Equinox.Models.Data.Configuration
{
    public class ConfigureClubs : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> entity)
        {
            // Configure primary key
            entity.HasKey(c => c.ClubId);

            // Configure relationships
            entity.HasMany(c => c.EquinoxClasses)
                  .WithOne(ec => ec.Club)
                  .HasForeignKey(ec => ec.ClubId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            entity.HasData(
                new Club { ClubId = 1, Name = "Chicago Loop", PhoneNumber = "1234567890" },
                new Club { ClubId = 2, Name = "West Chicago", PhoneNumber = "9876543210" }
            );
        }
    }
}
