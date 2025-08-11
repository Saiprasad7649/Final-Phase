using Equinox.Models.DomainModels;

namespace Equinox.Models.Data.Repository
{
    public interface IClassCategoryRepository : IRepository<ClassCategory>
    {
        // Add any ClassCategory-specific repository methods here
        bool HasBookedClasses(int classCategoryId);
    }
}
