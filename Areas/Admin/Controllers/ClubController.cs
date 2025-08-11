using Microsoft.AspNetCore.Mvc;
using Equinox.Models.Data.Repository;
using Equinox.Models.DomainModels;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        public ClubController(IClubRepository clubRepository) => _clubRepository = clubRepository;

        // LIST
        public IActionResult Index()
        {
            var clubs = _clubRepository.List(new Models.Data.QueryOptions<Club>());
            return View(clubs);
        }

        // CREATE: GET
        public IActionResult Create()
        {
            return View(new Club());
        }

        // CREATE: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Club club)
        {
            if (!ModelState.IsValid) 
            {
                TempData["ModelError"] = "Please fix the error";
                return View(club);
            }
            _clubRepository.Insert(club);
            _clubRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // EDIT: GET
        public IActionResult Edit(int id)
        {
            var club = _clubRepository.Get(id);
            if (club == null) return NotFound();
            return View("Create", club);
        }

        // EDIT: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Club club)
        {
            if (!ModelState.IsValid) 
            {
                TempData["ModelError"] = "Please fix the error";
                return View("Create", club);
            }
            _clubRepository.Update(club);
            _clubRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // DELETE: GET confirm
        public IActionResult Delete(int id)
        {
            var club = _clubRepository.Get(id);
            if (club == null) return NotFound();
            return View(club);
        }

        // DELETE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int clubId)
        {
            return DeleteClub(clubId);
        }

        private IActionResult DeleteClub(int clubId)
        {
            var club = _clubRepository.Get(clubId);
            if (club != null)
            {
                // Check if the club is referenced by any booked classes
                bool isReferenced = _clubRepository.HasBookedClasses(clubId);

                if (isReferenced)
                {
                    // Club is referenced by booked classes, don't delete
                    TempData["Error"] = $"Cannot delete club '{club.Name}' because it has booked classes.";
                    return RedirectToAction(nameof(Index));
                }

                _clubRepository.Delete(club);
                _clubRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
