using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Linq;

namespace Equinox.Models.Util
{
    public class Validate
    {
        private const string PhoneKey = "ValidatePhoneKey";
        private const string AgeKey = "ValidateAgeKey";
        
        private ITempDataDictionary tempData { get; set; }
        public bool IsValid { get; set; } = true;
        public string ErrorMessage { get; set; } = "";

        public Validate(ITempDataDictionary temp) => tempData = temp;

        public void CheckPhone(string phoneNumber, string userId)
        {
            // Skip validation if already checked
            if (tempData.Keys.Contains(PhoneKey)) return;
            
            // Note: The actual validation is now done in the UserRepository
            // This method is kept for compatibility with existing code
        }

        public void MarkPhoneChecked() => tempData[PhoneKey] = true;

        public void CheckAge(DateTime dob)
        {
            // Skip validation if already checked
            if (tempData.Keys.Contains(AgeKey)) return;

            // Calculate age
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            
            // Adjust age if birthday hasn't occurred yet this year
            if (dob.Date > today.AddYears(-age))
                age--;

            // Check if age is within valid range (8-80)
            if (age < 8 || age > 80)
            {
                IsValid = false;
                ErrorMessage = $"Age must be between 8 and 80";
            }
        }

        public void MarkAgeChecked() => tempData[AgeKey] = true;
    }
}
