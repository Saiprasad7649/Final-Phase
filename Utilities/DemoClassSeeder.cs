using System;
using System.Linq;
using Equinox.Models.Data;
using Equinox.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Equinox.Utilities
{
    public class DemoClassSeeder
    {
        public static void SeedDemoClasses()
        {
            using var context = new EquinoxContext(
                new DbContextOptionsBuilder<EquinoxContext>()
                    .UseSqlite("Data Source=Equinox.db")
                    .Options);

            // Get first available category, club, and user for demo
            var category = context.ClassCategories.FirstOrDefault();
            var club = context.Clubs.FirstOrDefault();
            var user = context.Users.FirstOrDefault();
            if (category == null || club == null || user == null)
            {
                Console.WriteLine("Cannot seed classes: missing category, club, or user.");
                return;
            }

            var demoClasses = new[]
            {
                new EquinoxClass { Name = "Barre Fusion", ClassPicture = "barre-fusion.jpg", ClassDay = "Monday", Time = "8 AM - 9 AM", ClassCategoryId = category.ClassCategoryId, ClubId = club.ClubId, UserId = user.UserId },
                new EquinoxClass { Name = "Boxing 101", ClassPicture = "boxing-101.jpg", ClassDay = "Tuesday", Time = "9 AM - 10 AM", ClassCategoryId = category.ClassCategoryId, ClubId = club.ClubId, UserId = user.UserId },
                new EquinoxClass { Name = "Hatha Yoga", ClassPicture = "hatha-yoga.jpg", ClassDay = "Wednesday", Time = "10 AM - 11 AM", ClassCategoryId = category.ClassCategoryId, ClubId = club.ClubId, UserId = user.UserId },
                new EquinoxClass { Name = "HIIT", ClassPicture = "hiit1.jpg", ClassDay = "Thursday", Time = "11 AM - 12 PM", ClassCategoryId = category.ClassCategoryId, ClubId = club.ClubId, UserId = user.UserId },
                new EquinoxClass { Name = "Morning Yoga", ClassPicture = "morning-yoga.jpg", ClassDay = "Friday", Time = "7 AM - 8 AM", ClassCategoryId = category.ClassCategoryId, ClubId = club.ClubId, UserId = user.UserId },
                new EquinoxClass { Name = "Power HIIT", ClassPicture = "power-hiit.jpg", ClassDay = "Saturday", Time = "12 PM - 1 PM", ClassCategoryId = category.ClassCategoryId, ClubId = club.ClubId, UserId = user.UserId },
                new EquinoxClass { Name = "Strength Training", ClassPicture = "strength-training.jpg", ClassDay = "Sunday", Time = "1 PM - 2 PM", ClassCategoryId = category.ClassCategoryId, ClubId = club.ClubId, UserId = user.UserId },
                new EquinoxClass { Name = "Yoga 2", ClassPicture = "yoga2.jpg", ClassDay = "Monday", Time = "2 PM - 3 PM", ClassCategoryId = category.ClassCategoryId, ClubId = club.ClubId, UserId = user.UserId }
            };

            foreach (var c in demoClasses)
            {
                if (!context.EquinoxClasses.Any(ec => ec.Name == c.Name))
                {
                    context.EquinoxClasses.Add(c);
                }
            }
            context.SaveChanges();
            Console.WriteLine("Demo classes seeded.");
        }
    }
}
