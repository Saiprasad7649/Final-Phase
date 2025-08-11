using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Equinox.Models.Util;

namespace Equinox.Models.DomainModels
{
    public class User
    {
    // UserId is auto-generated in controller
    public string UserId { get; set; } = string.Empty;
        
    [Required, StringLength(50), RegularExpression("^[a-zA-Z0-9 ]+$")]
    public string Name { get; set; } = string.Empty;
        
    [Required, Phone]
    [Remote("CheckPhone","Validation","Admin",
        AdditionalFields = nameof(UserId),
        ErrorMessage="Phone in use")]
    public string PhoneNumber { get; set; } = string.Empty;
        
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
        
    [Required, DataType(DataType.Date)]
    [AgeRange(8,80, ErrorMessage="Age must be 8–80")]
    [Remote("CheckAge","Validation","Admin",
        ErrorMessage="Age must be between 8 and 80")]
    public DateTime DOB { get; set; }
        
    public bool IsCoach { get; set; } = false;

    // Navigation property
    public ICollection<EquinoxClass> CoachedClasses { get; set; } = new List<EquinoxClass>();
    }
}
