using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Equinox.Models.DomainModels;

namespace Equinox.Models.Data.Configuration
{
    public class ConfigureEquinoxClasses : IEntityTypeConfiguration<EquinoxClass>
    {
        public void Configure(EntityTypeBuilder<EquinoxClass> entity)
        {
            // Configure primary key
            entity.HasKey(ec => ec.EquinoxClassId);

            // Configure relationships
            entity.HasOne(ec => ec.Club)
                  .WithMany(c => c.EquinoxClasses)
                  .HasForeignKey(ec => ec.ClubId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ec => ec.ClassCategory)
                  .WithMany(cc => cc.EquinoxClasses)
                  .HasForeignKey(ec => ec.ClassCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ec => ec.User)
                  .WithMany(u => u.CoachedClasses)
                  .HasForeignKey(ec => ec.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(ec => ec.Bookings)
                  .WithOne(b => b.EquinoxClass)
                  .HasForeignKey(b => b.EquinoxClassId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            entity.HasData(
                new EquinoxClass {
                    EquinoxClassId = 1,
                    Name = "Morning Yoga",
                    ClassPicture = "yoga1.jpg",
                    ClassDay = "Monday",
                    Time = "8 AM – 9 AM",
                    ClassCategoryId = 1,
                    ClubId = 1,
                    UserId = "coach1"
                },
                new EquinoxClass {
                    EquinoxClassId = 2,
                    Name = "HIIT Workout",
                    ClassPicture = "hiit1.jpg",
                    ClassDay = "Tuesday",
                    Time = "5 PM – 6 PM",
                    ClassCategoryId = 2,
                    ClubId = 1,
                    UserId = "coach2"
                },
                new EquinoxClass {
                    EquinoxClassId = 3,
                    Name = "Cardio Blast",
                    ClassPicture = "cardio1.jpg",
                    ClassDay = "Wednesday",
                    Time = "12 PM – 1 PM",
                    ClassCategoryId = 3,
                    ClubId = 2,
                    UserId = "coach3"
                }
            );
        }
    }
}
