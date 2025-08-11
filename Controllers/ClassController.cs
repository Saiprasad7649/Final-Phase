using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Equinox.Models.Data.Repository;
using Equinox.Models.DomainModels;
using Equinox.Models.ViewModels;
using Equinox.Models.Data;
using Equinox.Utilities;
using System.Linq;

namespace Equinox.Controllers
{
    public class ClassController : Controller
    {
    private readonly IRepository<EquinoxClass> classRepository;
    private readonly IRepository<Club> clubRepository;
    private readonly IRepository<ClassCategory> categoryRepository;

        public ClassController(
            IRepository<EquinoxClass> classRepo,
            IRepository<Club> clubRepo,
            IRepository<ClassCategory> categoryRepo)
        {
            classRepository = classRepo;
            clubRepository = clubRepo;
            categoryRepository = categoryRepo;
        }

        public IActionResult List(string clubId, string categoryId)
        {
            // 1) Use the session‐wrapper to persist filter values
            var filterSession = new FilterSession(HttpContext.Session);

            if (!string.IsNullOrEmpty(clubId) || !string.IsNullOrEmpty(categoryId))
            {
                // store new values in session
                filterSession.ClubId     = clubId     ?? "all";
                filterSession.CategoryId = categoryId ?? "all";
            }
            else
            {
                // load last values from session
                clubId     = filterSession.ClubId;
                categoryId = filterSession.CategoryId;
            }

            // 2) Build the ViewModel
            var viewModel = new ClassViewModel
            {
                ActiveClubId     = clubId,
                ActiveCategoryId = categoryId,
                Clubs            = clubRepository.List(new QueryOptions<Club>()).ToList(),
                Categories       = categoryRepository.List(new QueryOptions<ClassCategory>()).ToList()
            };

            // 3) Apply filters
            var options = new QueryOptions<EquinoxClass>
            {
                Includes = "Club,ClassCategory,User"
            };
            if (clubId != "all")
                options.Where = c => c.Club.ClubId.ToString() == clubId;
            if (categoryId != "all")
                options.Where = c => c.ClassCategory.ClassCategoryId.ToString() == categoryId;
            viewModel.Classes = classRepository.List(options).ToList();

            return View(viewModel);
        }

        public IActionResult Details(int id, string clubId, string categoryId)
        {
            // Pass the active filters along for the Details view
            ViewBag.ActiveClubId     = clubId     ?? "all";
            ViewBag.ActiveCategoryId = categoryId ?? "all";

            var equinoxClass = classRepository.Get(new QueryOptions<EquinoxClass>
            {
                Where = c => c.EquinoxClassId == id,
                Includes = "Club,ClassCategory,User"
            });
            if (equinoxClass == null)
                return NotFound();
            return View(equinoxClass);
        }
    }
}
