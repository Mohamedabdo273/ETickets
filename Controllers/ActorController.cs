using ETickets.Models;
using ETickets.Repository.IRepository;
using ETickets.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Linq;

namespace ETickets.Controllers
{
    [Authorize(Roles = $"{SD.adminRole}")]
    public class ActorController : Controller
    {
        IActor actors;
        public ActorController(IActor actor)
        {
            this.actors = actor;
        }
        public IActionResult Index(string query)
        {
           var Actors = actors.Get();
            if (query != null)
            {
                Actors = actors.Get(expression: e => e.FirstName.ToUpper().Contains(query.ToUpper()) || e.LastName.ToUpper().Contains(query.ToUpper()));
                
            }       
            return View(Actors);
        }
        public IActionResult Actor(int id)
        {
            var actor = actors.Get(expression:e=>e.Id==id);
            return View(actor);
        }
        public IActionResult Create()
        {
            return View(new Actor());        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Actor actor, IFormFile PhotoUrl) 
        {
            if (ModelState.IsValid)
            {
                if (PhotoUrl != null && PhotoUrl.Length > 0) 
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PhotoUrl.FileName); 
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\cast", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        PhotoUrl.CopyTo(stream);
                    }

                    actor.ProfilePicture = fileName; 
                }
                actors.Create(actor); 
                actors.Commit(); 
                return RedirectToAction(nameof(Index)); 
            }
            return View(actor); 
        }

        public IActionResult Edit(int id)
        {
            var Actor = actors.GetOne(expression: e => e.Id == id);
            if (Actor == null)
                return View(nameof(NotFound));
             return View(Actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Actor actor, IFormFile PhotoUrl)
        {
            if (ModelState.IsValid)
            {
                var oldProduct = actors.GetOne(expression: e => e.Id == actor.Id, tracked: false);
                ModelState.Remove("PhotoUrl");
                if (PhotoUrl != null && PhotoUrl.Length > 0) // 85896
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PhotoUrl.FileName); // "0283dasda2032-321321983lkjwlkds.png"

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\cast", fileName);

                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\cast", oldProduct.ProfilePicture);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        PhotoUrl.CopyTo(stream);
                    }

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    actor.ProfilePicture = fileName;
                }
                else
                {
                    actor.ProfilePicture = oldProduct.ProfilePicture;
                }

                actors.Edit(actor);
                actors.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        public IActionResult Delete(int id)
        {
            var Actor = actors.GetOne(expression: e =>e.Id == id);
            if (Actor == null)
                return View(nameof(NotFound));
           
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\cast", Actor.ProfilePicture);

            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
           
            actors.Delete(Actor);
            actors.Commit();
            return RedirectToAction("Index");
        }
        
    }
}
