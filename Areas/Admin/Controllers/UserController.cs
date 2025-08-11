using Microsoft.AspNetCore.Mvc;
using Equinox.Models.Data.Repository;
using Equinox.Models.DomainModels;
using System;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository) => _userRepository = userRepository;

        // LIST
        public IActionResult Index()
        {
            var users = _userRepository.List(new Models.Data.QueryOptions<User>());
            return View(users);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            ViewBag.Action = "Add";
            return View(new User());
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                TempData["ModelError"] = "Please fix the error.";
                ViewBag.Action = "Add";
                return View(user);
            }

            // Generate a unique UserId for new users
            if (string.IsNullOrEmpty(user.UserId))
            {
                // Generate a unique ID based on name and timestamp
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string namePrefix = !string.IsNullOrEmpty(user.Name) ? user.Name.Substring(0, Math.Min(3, user.Name.Length)).ToLower() : "usr";
                user.UserId = $"{namePrefix}{timestamp}";
            }
            
            _userRepository.Insert(user);
            _userRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET)
        public IActionResult Edit(string id)
        {
            var user = _userRepository.Get(id);
            if (user == null) return NotFound();
            ViewBag.Action = "Edit";
            return View("Create", user);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                var errorFields = string.Join(", ", ModelState.Keys
                    .Where(k => ModelState[k].Errors.Count > 0)
                    .Select(k => k.Contains(".") ? k.Split(".").Last() : k));
                TempData["ModelError"] = $"Please fix the errors in these fields: {errorFields}";
                ViewBag.Action = "Edit";
                return View("Create", user);
            }

            _userRepository.Update(user);
            _userRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // DELETE (GET)
        public IActionResult Delete(string id)
        {
            var user = _userRepository.Get(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // DELETE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string userId)
        {
            return DeleteUser(userId);
        }

        private IActionResult DeleteUser(string userId)
        {
            var user = _userRepository.Get(userId);
            if (user != null)
            {
                bool isReferenced = _userRepository.HasBookedClasses(userId);

                if (isReferenced)
                {
                    TempData["Error"] = $"Cannot delete {(user.IsCoach ? "coach" : "user")} '{user.Name}' because they are coaching classes with bookings or have their own bookings.";
                    return RedirectToAction(nameof(Index));
                }

                _userRepository.Delete(user);
                _userRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        // REMOTE VALIDATION moved to ValidationController
        // This method is kept for reference but is no longer used
        /*
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyPhone(string phoneNumber, string userId)
        {
            bool taken = _context.Users.Any(u => u.PhoneNumber == phoneNumber
                                          && u.UserId != userId);
            return taken
                ? Json($"Phone {phoneNumber} already in use")
                : Json(true);
        }
        */
    }
}
