using Equinox.Models.DomainModels;

namespace Equinox.Models.Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        // Add any User-specific repository methods here
        bool HasBookedClasses(string userId);
        
        // Check if a phone number is already in use by another user
        bool PhoneNumberExists(string phoneNumber, string userId);
    }
}
