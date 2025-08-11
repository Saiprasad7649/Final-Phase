using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Equinox.Models.DomainModels;

namespace Equinox.Models.Data.Configuration
{
    public class ConfigureUsers : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            // Configure primary key
            entity.HasKey(u => u.UserId);

            // Configure relationships
            entity.HasMany(u => u.CoachedClasses)
                  .WithOne(ec => ec.User)
                  .HasForeignKey(ec => ec.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            entity.HasData(
                new User {
                    UserId      = "coach1",
                    Name        = "Jane Coach",
                    PhoneNumber = "5551234567",
                    Email       = "jane@example.com",
                    DOB         = new DateTime(1990, 1, 1),
                    IsCoach     = true
                },
                new User {
                    UserId      = "coach2",
                    Name        = "Sarah",
                    PhoneNumber = "5552345678",
                    Email       = "sarah@example.com",
                    DOB         = new DateTime(1988, 2, 2),
                    IsCoach     = true
                },
                new User {
                    UserId      = "coach3",
                    Name        = "George",
                    PhoneNumber = "5553456789",
                    Email       = "george@example.com",
                    DOB         = new DateTime(1985, 3, 3),
                    IsCoach     = true
                },
                new User {
                    UserId      = "coach4",
                    Name        = "Henry",
                    PhoneNumber = "5554567890",
                    Email       = "henry@example.com",
                    DOB         = new DateTime(1982, 4, 4),
                    IsCoach     = true
                },
                new User {
                    UserId      = "coach5",
                    Name        = "Lena",
                    PhoneNumber = "5555678901",
                    Email       = "lena@example.com",
                    DOB         = new DateTime(1991, 5, 5),
                    IsCoach     = true
                }
            );
        }
    }
}
