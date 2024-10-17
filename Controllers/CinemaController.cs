using ETickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Controllers
{
    public class CinemaController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public IActionResult Index()
        {
            var cinema=db.Cinemas.ToList();
            return View(cinema);
        }
        public IActionResult AllCinemas(int id)
        {
            var cinema = db.Movies.Include(e=>e.Cinema).Include(e=>e.Category).Where(e=>e.CinemaId==id);
            return View(cinema);
        }
        public IActionResult MoreDetails(int id)
        {
            var cinema = db.Cinemas.Find(id);
            return View(cinema);
        }
    }
}
