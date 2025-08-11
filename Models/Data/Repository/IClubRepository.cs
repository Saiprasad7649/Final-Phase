using Equinox.Models.DomainModels;

namespace Equinox.Models.Data.Repository
{
    public interface IClubRepository : IRepository<Club>
    {
        // Add any Club-specific repository methods here
        bool HasBookedClasses(int clubId);
    }
}
