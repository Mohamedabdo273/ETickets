using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ETickets.Data;
using ETickets.Models;
using ETickets.Repository.IRepository;

namespace ETickets.Controllers
{
    public class ActorMoviesController : Controller
    {
        
        private readonly IActorMovie actorMovie;
        private readonly IMovie movie;
        private readonly IActor actor;

        public ActorMoviesController(IActorMovie actorMovie,IMovie movie,IActor actor)
        {
           
            this.actorMovie = actorMovie;
            this.movie = movie;
            this.actor = actor;
        }

        
        public IActionResult Index()
        {
            var applicationDbContext = actorMovie.Get([e=>e.Movie,e=>e.Actor]);
            return View(applicationDbContext);
        }

        // GET: ActorMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ActorMovie = actorMovie.Get([a => a.Actor, a => a.Movie]).FirstOrDefault(m => m.MovieId == id);
                
            if (ActorMovie == null)
            {
                return RedirectToAction(nameof(NotFound));
            }

            return View(actorMovie);
        }

        public IActionResult Create()
        {
            ViewBag.Actors = new SelectList(actor.Get(), "Id", "FullName");
            ViewBag.Movies = new SelectList(movie.Get(), "Id", "Name");
            return View(new ActorMovie());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ActorMovie ActorMovie)
        {
            var duplicate = actorMovie.GetOne(expression :e => e.ActorId == ActorMovie.ActorId && e.MovieId == ActorMovie.MovieId);

            if (duplicate != null)
            {
                TempData["Error"] = "This actor is already associated with this movie.";
                return RedirectToAction(nameof(Index));
            }


            if (ModelState.IsValid)
            {
                actorMovie.Create(ActorMovie);
                actorMovie.Commit();
                return RedirectToAction(nameof(Index));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            ViewBag.Actors = new SelectList(actor.Get(), "Id", "FullName");
            ViewBag.Movies = new SelectList(movie.Get(), "Id", "Name");

            return View(actorMovie);
        }



        public IActionResult Edit(int id)
        {
            var actorMovieItem = actorMovie.Get(expression :e => e.Id==id).FirstOrDefault();

            if (actorMovieItem == null)
            {
                return NotFound();
            }

            ViewBag.Actors = new SelectList(actor.Get(), "Id", "FullName");
            ViewBag.Movies = new SelectList(movie.Get(), "Id", "Name");
            return View(actorMovieItem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ActorMovie actorMovieModel)
        {
            var duplicate = actorMovie.GetOne(expression :e =>
                e.ActorId == actorMovieModel.ActorId &&
                e.MovieId == actorMovieModel.MovieId &&
                (e.ActorId != actorMovieModel.ActorId || e.MovieId != actorMovieModel.MovieId)
            );

            if (duplicate != null)
            {
                TempData["Error"] = "This actor is already associated with this movie.";
                return RedirectToAction(nameof(Index));
            }


            if (ModelState.IsValid)
            {
                
                actorMovie.Edit(actorMovieModel);
                actorMovie.Commit();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Actors = new SelectList(actor.Get(), "Id", "FullName");
            ViewBag.Movies = new SelectList(movie.Get(), "Id", "Name");

            return View(actorMovieModel);
        }



        public IActionResult Delete(int id)
        {
            var ActorMovie = actorMovie.GetOne(expression: e => e.Id==id);
            if (ActorMovie == null)
            {
                return RedirectToAction(nameof(NotFound));
            }
            actorMovie.Delete(ActorMovie);
            actorMovie.Commit();
           
            return RedirectToAction(nameof(Index));
        }

    }
}
