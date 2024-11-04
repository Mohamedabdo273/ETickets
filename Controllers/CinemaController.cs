using ETickets.Data;
using ETickets.Models;
using ETickets.Repository.IRepository;
using ETickets.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ETickets.Controllers
{
   
    public class CinemaController : Controller
    {
        ICinema Cinema { get; set; }
        IMovie movie   { get; set; }

        public CinemaController(ICinema cinema, IMovie movie)
        {
            this.Cinema = cinema;
            this.movie = movie;
        }
        [Authorize(Roles = $"{SD.adminRole}")]
        public IActionResult Index(string? query)
        {
            var cinema = Cinema.Get();
            if (query != null)
            {
                cinema = Cinema.Get(expression: e => e.Name.ToUpper().Contains(query.ToUpper()));

            }
            return View(cinema);
        }
        [Authorize(Roles = $"{SD.adminRole}")]
        public IActionResult AllCinemas(int id)
        {
            var cinema = movie.Get([e => e.Cinema, e => e.Category], e => e.CinemaId == id);
            return View(cinema);
        }
        [Authorize(Roles = $"{SD.CustomerRole},{SD.adminRole}")]
        public IActionResult MoreDetails(int id)
        {
            var cinema = Cinema.GetOne(expression:e=>e.Id==id);
            return View(cinema);
        }
        [Authorize(Roles = $"{SD.adminRole}")]
        public IActionResult Create()
        {
            Cinema cinema = new Cinema();
            return View(cinema);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cinema cinema ,IFormFile PhotoUrl)
        {
            if (ModelState.IsValid)
            {
                ModelState.Remove("PhotoUrl");
                if (PhotoUrl != null && PhotoUrl.Length > 0) // 85896
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PhotoUrl.FileName); // "0283dasda2032-321321983lkjwlkds.png"

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Logo", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        PhotoUrl.CopyTo(stream);
                    }

                    
                    cinema.CinemaLogo = fileName;
                }
                else
                {
                    cinema.CinemaLogo = " ";
                }

                Cinema.Create(cinema);
                Cinema.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }
        [Authorize(Roles = $"{SD.adminRole}")]
        public IActionResult Edit(int id)
        {
            var cinema = Cinema.GetOne(expression: e => e.Id == id);
            if (cinema == null)
                return View(nameof(NotFound));           
            return View(cinema);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cinema cinema ,IFormFile PhotoUrl)
        {
            ModelState.Remove("PhotoUrl");
            if (ModelState.IsValid)
            {
                var oldProduct = Cinema.GetOne(expression: e => e.Id == cinema.Id, tracked: false);
                ModelState.Remove("PhotoUrl");
                if (PhotoUrl != null && PhotoUrl.Length > 0) // 85896
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PhotoUrl.FileName); // "0283dasda2032-321321983lkjwlkds.png"

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Logo", fileName);

                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Logo", oldProduct.CinemaLogo);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        PhotoUrl.CopyTo(stream);
                    }

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    cinema.CinemaLogo = fileName;
                }
                else
                {
                    cinema.CinemaLogo = oldProduct.CinemaLogo;
                }

                Cinema.Edit(cinema);
                Cinema.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }
        [Authorize(Roles = $"{SD.adminRole}")]
        public IActionResult Delete(int id)
        {
            var cinema = Cinema.GetOne(expression: e => e.Id == id);
            if(cinema !=null)
            {
                Cinema.Delete(cinema);
                Cinema.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(NotFound));
        }
    }
}
