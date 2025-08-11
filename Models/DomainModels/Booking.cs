using System.ComponentModel.DataAnnotations;

namespace Equinox.Models.DomainModels
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public int EquinoxClassId { get; set; }
        public EquinoxClass? EquinoxClass { get; set; }

        // New: User who made the booking
    public string? UserId { get; set; }
    public User? User { get; set; }
    }
}
