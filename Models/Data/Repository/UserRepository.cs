using System.Linq;
using Equinox.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Equinox.Models.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(EquinoxContext context) : base(context) { }

        public bool HasBookedClasses(string userId)
        {
            // Check if user is a coach of any class with bookings
            bool isCoachWithBookings = context.EquinoxClasses
                .Include(ec => ec.Bookings)
                .Any(ec => ec.UserId == userId && ec.Bookings.Any());

            // Check if user has any bookings as a member (assuming Booking has a UserId property)
            bool hasOwnBookings = context.Bookings.Any(b => b.UserId == userId);

            return isCoachWithBookings || hasOwnBookings;
        }

        public bool PhoneNumberExists(string phoneNumber, string userId)
        {
            return context.Users.Any(u => u.PhoneNumber == phoneNumber && u.UserId != userId);
        }
    }
}
