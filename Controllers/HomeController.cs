using ETickets.Data;
using ETickets.Models;
using ETickets.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ETickets.Controllers
{
    public class HomeController : Controller
    {
         IMovie movie;
         IActor actor;

        public HomeController(IMovie movie,IActor actor)
        {
            this.movie = movie;
            this.actor = actor;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            var movies = movie.Get([e => e.Cinema, e => e.Category]);
            int totalMovies = movies.Count();
            int totalPages = (int)Math.Ceiling((double)totalMovies / pageSize);

            if (pageNumber < 1 || pageNumber > totalPages)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var pagedMovies = movies.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Pass pagination details through ViewBag
            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.HasPreviousPage = pageNumber > 1;
            ViewBag.HasNextPage = pageNumber < totalPages;
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            return View(pagedMovies); // Directly pass the paged movie list to the view
        }

        public IActionResult MoreDetails(int id)
        { 
            var movies = movie.GetWithIncludes(e=>e.Include(e=>e.Cinema)
            .Include(e=>e.Category).Include(e=>e.Actors).ThenInclude(e=>e.Actor),e=>e.Id==id).FirstOrDefault();
            if(movies == null)
                return View(nameof(NotFound));
            return View(movies);
        }
        public IActionResult BookTicket(int id)
        {
            var movies = movie.GetWithIncludes(e => e.Include(e => e.Cinema)
            .Include(e => e.Category).Include(e => e.Actors).ThenInclude(e => e.Actor), e => e.Id == id).FirstOrDefault();
            if (movies == null)
                return View(nameof(NotFound));
            return View(movies);
        }
        public IActionResult Actor(int id)
        {
            var actorDetails = actor.GetWithIncludes(e => e.Include(e => e.ActorsMovie).ThenInclude(e => e.Movie), e => e.Id == id)
                                     .FirstOrDefault();

            if (actorDetails == null)
            {
                return View(nameof(NotFound));
            }

            return View(actorDetails);
        }
        public IActionResult Search(string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {

                return View(new List<Movie>());
            }

            var movies = movie.GetWithIncludes(e => e.Include(m => m.Cinema)
                                  .Include(m => m.Category),e=>e.Name.ToLower().Contains(query.ToUpper()));
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
