using System.ComponentModel.DataAnnotations;

namespace Equinox.Models.DomainModels
{
    public class Club
    {
        public int ClubId { get; set; }
        
        [Required(ErrorMessage="Required")]
        [StringLength(50, ErrorMessage="Max 50 chars")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage="Alphanumeric only")]
        public string Name { get; set; } = string.Empty;
        
        [Phone(ErrorMessage="Invalid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        // Navigation property
        public ICollection<EquinoxClass> EquinoxClasses { get; set; } = new List<EquinoxClass>();
    }
}
