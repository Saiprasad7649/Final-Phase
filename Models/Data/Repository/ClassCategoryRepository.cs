using System.Linq;
using Equinox.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Equinox.Models.Data.Repository
{
    public class ClassCategoryRepository : Repository<ClassCategory>, IClassCategoryRepository
    {
        public ClassCategoryRepository(EquinoxContext context) : base(context) { }

        public bool HasBookedClasses(int classCategoryId)
        {
            return context.EquinoxClasses
                .Include(ec => ec.Bookings)
                .Any(ec => ec.ClassCategoryId == classCategoryId && ec.Bookings.Any());
        }
    }
}
