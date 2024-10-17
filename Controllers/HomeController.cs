using ETickets.Data;
using ETickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ETickets.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
         ApplicationDbContext db = new ApplicationDbContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var movies = db.Movies.Include(e=>e.Cinema).Include(e=>e.Category).ToList();
            return View(movies);
        }
        public IActionResult MoreDetails(int id)
        {
            var movies = db.Movies.Include(e => e.Cinema).Include(e => e.Category).Include(e=>e.Actors).ThenInclude(am => am.Actor).FirstOrDefault(e=>e.Id==id);
            return View(movies);
        }
        public IActionResult Actor(int id)
        {
            
            var actor = db.Actors.Include(e=>e.ActorsMovie).ThenInclude(e=>e.Movie).FirstOrDefault(e=>e.Id == id);
            
            return View(actor);
        }
        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
               
                return View(new List<Movie>());
            }


            var movies = db.Movies.Include(m => m.Cinema)
                                  .Include(m => m.Category)
                                  .Where(m => m.Name.ToLower().Contains(query.ToUpper()));
                                  

            return View(movies);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
