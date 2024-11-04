using ETickets.Data;
using ETickets.Models;
using ETickets.Repository;
using ETickets.Repository.IRepository;
using ETickets.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ETickets.Controllers
{
    [Authorize(Roles = $"{SD.adminRole}")]
    public class CategoryController : Controller
    {
         ICategory category;
         IMovie movie;

        public CategoryController(ICategory category,IMovie movie)
        {
            this.category = category;
            this.movie = movie;
        }
        public IActionResult Index(string? query)
        {
           var categories= category.Get();
           
            if (query != null)
            {
                categories = category.Get(expression: e => e.Name.ToUpper().Contains(query.ToUpper()));

            }
            return View(categories);
        }
       
        [AllowAnonymous]
        public IActionResult AllCategory(int id)
        {

            var movies = movie.Get([e => e.Category, e => e.Cinema], e => e.CategoryId == id);
            return View(movies);
        }
        [Authorize(Roles = $"{SD.adminRole}")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category categories)
        { 
            if(ModelState.IsValid)
            {
               category.Create(categories);
                category.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
          
        }
        
        public IActionResult Edit(int id)
        {
            var categories = category.GetOne(expression: e => e.Id == id);
            if (category != null)
                return View(model: categories);
            return View(nameof(NotFound));
          
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category categories)
        {
            if (ModelState.IsValid)
            {
                category.Edit(categories);
                category.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(categories);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Delete(int id)
        {
            var Categories = category.GetOne(expression: e => e.Id == id);
            if (Categories == null)
                return RedirectToAction(nameof(NotFound));

            category.Delete(Categories);
            category.Commit();
            return RedirectToAction(nameof(Index));

        }


    }
}
