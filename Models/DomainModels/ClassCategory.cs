using System.ComponentModel.DataAnnotations;

namespace Equinox.Models.DomainModels
{
    public class ClassCategory
    {
        public int ClassCategoryId { get; set; }
        
        [Required(ErrorMessage="Required")]
        [StringLength(50, ErrorMessage="Max 50 chars")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage="Alphanumeric only")]
        public string Name { get; set; } = string.Empty;
        
        // Image property - string type for image file name, no validation required
        public string Image { get; set; } = string.Empty;

        // Navigation property
        public ICollection<EquinoxClass> EquinoxClasses { get; set; } = new List<EquinoxClass>();
    }
}
