using ETickets.Data;
using ETickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public IActionResult Index()
        {
            var category = db.Categories.ToList();
            return View(category);
        }
        public IActionResult AllCategory(int id)
        {
            var movies = db.Movies.Include(e => e.Category)
                                  .Include(e => e.Cinema)
                                  .Where(e => e.CategoryId == id);
                                  
            return View(movies);
        }
    }
}
