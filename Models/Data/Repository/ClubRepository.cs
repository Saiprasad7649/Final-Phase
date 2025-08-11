using System.Linq;
using Equinox.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Equinox.Models.Data.Repository
{
    public class ClubRepository : Repository<Club>, IClubRepository
    {
        public ClubRepository(EquinoxContext context) : base(context) { }

        public bool HasBookedClasses(int clubId)
        {
            return context.EquinoxClasses
                .Include(ec => ec.Bookings)
                .Any(ec => ec.ClubId == clubId && ec.Bookings.Any());
        }
    }
}
