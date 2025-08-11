using Microsoft.AspNetCore.Mvc;
using Equinox.Models.Data.Repository;
using Equinox.Models.DomainModels;

namespace Equinox.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClassCategoryController : Controller
    {
        private readonly IClassCategoryRepository _categoryRepository;
        public ClassCategoryController(IClassCategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        // LIST
        public IActionResult Index()
        {
            var categories = _categoryRepository.List(new Models.Data.QueryOptions<ClassCategory>());
            return View(categories);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View(new ClassCategory());
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClassCategory category)
        {
            if (!ModelState.IsValid) 
            {
                TempData["ModelError"] = "Please fix the error";
                return View(category);
            }

            _categoryRepository.Insert(category);
            _categoryRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET)
        public IActionResult Edit(int id)
        {
            var category = _categoryRepository.Get(id);
            if (category == null) return NotFound();

            return View("Create", category);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClassCategory category)
        {
            if (!ModelState.IsValid) 
            {
                TempData["ModelError"] = "Please fix the error";
                return View("Create", category);
            }

            _categoryRepository.Update(category);
            _categoryRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // DELETE (GET)
        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.Get(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // DELETE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int classCategoryId)
        {
            return DeleteCategory(classCategoryId);
        }

        private IActionResult DeleteCategory(int classCategoryId)
        {
            var category = _categoryRepository.Get(classCategoryId);
            if (category != null)
            {
                // Check if the category is referenced by any booked classes
                bool isReferenced = _categoryRepository.HasBookedClasses(classCategoryId);

                if (isReferenced)
                {
                    // Category is referenced by booked classes, don't delete
                    TempData["Error"] = $"Cannot delete category '{category.Name}' because it has booked classes.";
                    return RedirectToAction(nameof(Index));
                }

                _categoryRepository.Delete(category);
                _categoryRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
