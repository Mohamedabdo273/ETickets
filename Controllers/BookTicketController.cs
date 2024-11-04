using ETickets.Migrations;
using ETickets.Models;
using ETickets.Repository;
using ETickets.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ETickets.Utility;

namespace ETickets.Controllers
{
    public class BookTicketController : Controller
    {
        private readonly ICard card;
        private readonly UserManager<ApplicationUsers> userManager;
        private readonly IMovie movie;
        private readonly IOrder order;

        public BookTicketController(ICard card,UserManager<ApplicationUsers> userManager,IMovie movie,IOrder order)
        {
            this.card = card;
            this.userManager = userManager;
            this.movie = movie;
            this.order = order;
        }
        public IActionResult Index()
        {
            var appUser = userManager.GetUserId(User);

            var shoppingCarts = card.Get([e => e.Movie, e => e.ApplicationUsers], e => e.UserId == appUser);

            ViewBag.TotalPrice = shoppingCarts.Sum(e => e.Movie.Price *e.count);
            
            return View(shoppingCarts);
        }
        public IActionResult AddToCart(int MovieId,int Count)
        {
            var appUser = userManager.GetUserId(User);
            var Tickets = movie.Get(expression : e=>e.Id==MovieId).FirstOrDefault();
            if (Tickets == null)
                return RedirectToAction(nameof(NotFound));
      
            if ( Count==null || Count<=0 || Tickets.quantity < Count)
            {
                return RedirectToAction("Index", "Home");
            }
            if (appUser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Cards cart = new Cards()
            {
                   count=Count,
                   MovieId=MovieId,
                   UserId=appUser
            };

            var cartDB = card.GetOne(expression: e => e.MovieId == MovieId && e.UserId == appUser);

            if (cartDB == null)
                card.Create(cart);
            else
                cartDB.count += Count;

            card.Commit();
            TempData["SuccessMessage"] = "Added product to cart successfully";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Increment(int id)
        {
            var appUser = userManager.GetUserId(User);
            var Movie = card.GetOne(expression:e=>e.MovieId==id && e.UserId==appUser);
            if(Movie !=null)
            {
                Movie.count++;
            }
            card.Commit();
            return RedirectToAction("Index");
        }
        public IActionResult Decrement(int id)
        {
            var appUser = userManager.GetUserId(User);
            var Movie = card.GetOne(expression: e => e.MovieId == id && e.UserId == appUser);
            if (Movie != null)
            {
                Movie.count--;
                if (Movie.count == 0)
                    card.Delete(Movie);
            }
            card.Commit();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var appUser = userManager.GetUserId(User);
            var Movie=card.GetOne(expression:e=>e.MovieId == id && e.UserId==appUser);
            if(Movie != null)
            {
                card.Delete(Movie);
                card.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(NotFound));
        }
        public IActionResult Pay()
        {
            var appUser = userManager.GetUserId(User);
            var cards = card.Get([e=>e.Movie],expression :e=>e.UserId == appUser).ToList();
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),            
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/BookTicket/Success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/BookTicket/Cancel",
            };
            foreach (var item in cards)
            {
                var result = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Movie.Name,
                        },
                        UnitAmount = (long)item.Movie.Price * 100,
                    },
                    Quantity = item.count,
                };
                options.LineItems.Add(result);
            }            
            var service = new SessionService();
            var session = service.Create(options);
            return Redirect(session.Url);
        }
       
        public IActionResult Success()
        {
            var appUser = userManager.GetUserId(User);
            var appUserName = userManager.GetUserName(User);
            var cards = card.GetWithIncludes(e => e.Include(e => e.Movie), expression: e => e.UserId == appUser);

            foreach (var item in cards)
            {
                var movie = item.Movie;

               
                if (movie != null && movie.quantity >= item.count)
                {
                    movie.quantity -= item.count; 

                   
                    var newOrder = new Orders
                    {
                        UserName = appUserName,
                        OrderId = movie.Id,
                        OrderName = movie.Name,
                        Count = item.count,
                        Date=DateTime.Now
                    };
                    order.Create(newOrder);
                }
            }

            
            card.Commit();
            order.Commit();  

            foreach (var item in cards)
            {
                card.Delete(item);
            }
            card.Commit();
            return View();
        }
        [Authorize(Roles = $"{SD.adminRole}")]
        public IActionResult Order()
        {
            var Order = order.Get();
            return View(Order);
        }
        public IActionResult Cancel()
        {
            return View();
        }
    }
    
}
