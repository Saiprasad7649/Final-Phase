using Microsoft.AspNetCore.Mvc;
using Equinox.Models.Data.Repository;
using Equinox.Models.DomainModels;
using Equinox.Models.Util;
using System;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        private readonly IUserRepository _userRepository;

        public ValidationController(IUserRepository userRepository) => _userRepository = userRepository;

        public JsonResult CheckPhone(string phoneNumber, string userId)
        {
            var validate = new Validate(TempData);
            
            // Check if phone number is already in use by another user
            bool phoneExists = _userRepository.PhoneNumberExists(phoneNumber, userId);
            if (phoneExists)
            {
                validate.ErrorMessage = $"Phone {phoneNumber} already in use";
                validate.IsValid = false;
            }
            else
            {
                validate.IsValid = true;
            }
            
            validate.MarkPhoneChecked();

            if (validate.IsValid)
            {
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

        public JsonResult CheckDOB(DateTime dob)
        {
            var validate = new Validate(TempData);
            validate.CheckAge(dob);
            validate.MarkAgeChecked();

            if (validate.IsValid)
            {
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }
    }
}