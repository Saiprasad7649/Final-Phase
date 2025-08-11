using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Equinox.Models.Data;
using Equinox.Models.DomainModels;

namespace Equinox.Utilities
{
    public class BookingUserIdAssigner
    {
        public static void AssignSequentialUserIds()
        {
            using var context = new EquinoxContext(
                new DbContextOptionsBuilder<EquinoxContext>()
                    .UseSqlite("Data Source=Equinox.db")
                    .Options);

            var users = context.Users.OrderBy(u => u.UserId).ToList();
            var bookings = context.Bookings.OrderBy(b => b.BookingId).ToList();

            if (users.Count < bookings.Count)
            {
                throw new Exception($"Not enough users ({users.Count}) for bookings ({bookings.Count})");
            }

            for (int i = 0; i < bookings.Count; i++)
            {
                bookings[i].UserId = users[i].UserId;
            }

            context.SaveChanges();
            Console.WriteLine("Assigned UserIds to all bookings.");
        }
    }
}
