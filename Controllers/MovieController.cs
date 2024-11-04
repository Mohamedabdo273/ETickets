using ETickets.Models;
using ETickets.Repository.IRepository;
using ETickets.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ETickets.Controllers
{
    [Authorize(Roles = $"{SD.adminRole}")]
    public class MovieController : Controller
    {
        private readonly IMovie movie;
        private readonly ICategory category1;
        private readonly IActor actor1;
        private readonly ICinema cinema1;

        public MovieController(IMovie movie,ICategory category,IActor actor,ICinema cinema)
        {
            this.movie = movie;
            this.category1 = category;
            this.actor1 = actor;
            this.cinema1 = cinema;
        }

        // GET: MovieController
        public ActionResult Index(string? query)
        {
            var Movie = movie.Get([e => e.Cinema, e => e.Category ]);
            if(query !=null)
            {
                Movie = movie.Get([e => e.Cinema, e => e.Category], e => e.Name.ToUpper().Contains(query.ToUpper()));
            }
            return View(Movie);
        }

        // GET: MovieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            ViewBag.category = new SelectList(category1.Get(), "Id", "Name");
            ViewBag.cinema = new SelectList(cinema1.Get(), "Id", "Name");
            ViewBag.actor = actor1.Get();
            ViewBag.movieStatus = new SelectList(Enum.GetValues(typeof(MovieStatus)));
            return View(new Movie());
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie Movie,IFormFile PhotoUrl, IFormFile TrailerUrl)
        {
            if(ModelState.IsValid)
            {
                if (PhotoUrl != null && PhotoUrl.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PhotoUrl.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        PhotoUrl.CopyTo(stream);
                    }

                    Movie.ImgUrl = fileName;
                }
                else
                {
                    Movie.ImgUrl = " ";
                }
                if (TrailerUrl != null && TrailerUrl.Length > 0)
                {
                    var trailerFileName = Guid.NewGuid().ToString() + Path.GetExtension(TrailerUrl.FileName);
                    var trailerFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", trailerFileName);

                    using (var stream = System.IO.File.Create(trailerFilePath))
                    {
                        TrailerUrl.CopyTo(stream);
                    }

                    Movie.TrailerUrl = trailerFileName;
                }
                else
                {
                    Movie.TrailerUrl = " ";
                }
                movie.Create(Movie);
                movie.Commit();
                return RedirectToAction(nameof(Index));

            }
            ViewBag.category = new SelectList(category1.Get(), "Id", "Name");
            ViewBag.cinema = new SelectList(cinema1.Get(), "Id", "Name");
            return View(movie);
        }

        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
                var movieToEdit = movie.GetOne(expression :e => e.Id == id); 

                if (movieToEdit == null)
                {
                    return NotFound(nameof(NotFound));
                }

                ViewBag.category = new SelectList(category1.Get(), "Id", "Name");
                ViewBag.cinema = new SelectList(cinema1.Get(), "Id", "Name");
                ViewBag.actor = actor1.Get();
                ViewBag.movieStatus = new SelectList(Enum.GetValues(typeof(MovieStatus)));

                return View(movieToEdit);
            
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movies,IFormFile PhotoUrl, IFormFile TrailerUrl)
        {
            var oldProduct = movie.GetOne(expression: e => e.Id == movies.Id, tracked: false);
            ModelState.Remove("PhotoUrl");
            ModelState.Remove("TrailerUrl");
            if (ModelState.IsValid)
            {
                if (PhotoUrl != null && PhotoUrl.Length > 0) // 85896
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PhotoUrl.FileName); // "0283dasda2032-321321983lkjwlkds.png"

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", fileName);

                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", oldProduct.ImgUrl);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        PhotoUrl.CopyTo(stream);
                    }

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    movies.ImgUrl = fileName;
                }
                else
                {
                    movies.ImgUrl = oldProduct.ImgUrl;
                }
                if (TrailerUrl != null && TrailerUrl.Length > 0)
                {
                    var trailerFileName = Guid.NewGuid().ToString() + Path.GetExtension(TrailerUrl.FileName);
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", oldProduct.TrailerUrl);
                    var trailerFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", trailerFileName);

                    using (var stream = System.IO.File.Create(trailerFilePath))
                    {
                        TrailerUrl.CopyTo(stream);
                    }
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                    movies.TrailerUrl = trailerFileName;
                }
                else
                {
                    movies.TrailerUrl = oldProduct.TrailerUrl;
                }

                movie.Edit(movies);
                movie.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.category = new SelectList(category1.Get(), "Id", "Name");
            ViewBag.cinema = new SelectList(cinema1.Get(), "Id", "Name");
            return View(movies);
        }


        // GET: MovieController/Delete/5
       
        public ActionResult Delete(int id)
        {
            var Movie = movie.GetOne(expression: e => e.Id == id);
            if (Movie == null)
                return NotFound();

            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", Movie.ImgUrl);
            var oldFilePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", Movie.TrailerUrl);

            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
            if (System.IO.File.Exists(oldFilePath2))
            {
                System.IO.File.Delete(oldFilePath2);
            }

            movie.Delete(Movie);
            movie.Commit();
            return RedirectToAction("Index");
        }


    }
}
