using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Equinox.Models.Data.Repository;
using Equinox.Models.DomainModels;
using Equinox.Models.Util;
using Equinox.Models.Data;
using Equinox.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Equinox.Controllers
{
    public class BookingController : Controller
    {
    private readonly IRepository<EquinoxClass> classRepository;

        public BookingController(IRepository<EquinoxClass> classRepo)
        {
            classRepository = classRepo;
        }

        // Accept the active filters, so the view can render a "Back to List" link
        public IActionResult Index(string clubId, string categoryId)
        {
            ViewBag.ActiveClubId     = clubId     ?? "all";
            ViewBag.ActiveCategoryId = categoryId ?? "all";

            // Get session bookings
            var sessionBookings = HttpContext.Session.GetObject<List<int>>("bookings");

            // Fallback to cookie if session is empty
            if (sessionBookings == null || sessionBookings.Count == 0)
            {
                var cookieHelper = new BookingCookies(Request.Cookies, Response.Cookies);
                sessionBookings = cookieHelper.GetBookings();
                HttpContext.Session.SetObject("bookings", sessionBookings);
            }

            // Use repository to get booked classes
            var bookedClasses = classRepository.List(new QueryOptions<EquinoxClass>
            {
                Where = c => sessionBookings.Contains(c.EquinoxClassId),
                Includes = "Club,ClassCategory,User"
            }).ToList();
            return View(bookedClasses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Book(int id, string clubId, string categoryId)
        {
            var bookings = HttpContext.Session.GetObject<List<int>>("bookings")
                           ?? new List<int>();

            if (!bookings.Contains(id))
                bookings.Add(id);

            // Save to session
            HttpContext.Session.SetObject("bookings", bookings);

            // Also save to cookie
            var cookieHelper = new BookingCookies(Request.Cookies, Response.Cookies);
            cookieHelper.SetBookings(bookings);

            // Confirmation message
            var selectedClass = classRepository.Get(new QueryOptions<EquinoxClass> { Where = c => c.EquinoxClassId == id });
            var className = selectedClass?.Name ?? "Class";
            var count     = bookings.Count;
            TempData["Message"] =
                $"{className} was successfully booked. You now have {count} class" +
                $"{(count > 1 ? "es" : "")} booked.";

            // Redirect back to the filtered Class list
            var filterSession = new FilterSession(HttpContext.Session);
            return RedirectToAction(
                "List",
                "Class",
                new { clubId = filterSession.ClubId, categoryId = filterSession.CategoryId }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id, string clubId, string categoryId)
        {
            var bookings = HttpContext.Session.GetObject<List<int>>("bookings")
                           ?? new List<int>();

            if (bookings.Contains(id))
                bookings.Remove(id);

            // Update both session and cookie
            HttpContext.Session.SetObject("bookings", bookings);
            var cookieHelper = new BookingCookies(Request.Cookies, Response.Cookies);
            cookieHelper.SetBookings(bookings);

            TempData["Message"] = "Class cancelled.";

            // Stay on the Booking page, but preserve filters for the "Back to List" link
            return RedirectToAction(
                nameof(Index),
                new { clubId, categoryId }
            );
        }
    }
}
