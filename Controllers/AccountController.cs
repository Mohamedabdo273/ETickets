using ETickets.Migrations;
using ETickets.Models;
using ETickets.Utility;
using ETickets.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace ETickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUsers> userManager;
        private readonly SignInManager<ApplicationUsers> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUsers> userManager,SignInManager<ApplicationUsers> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Register()
        {
            if (roleManager.Roles.IsNullOrEmpty())
            {
                await roleManager.CreateAsync(new(SD.adminRole));
                await roleManager.CreateAsync(new(SD.companyRole));
                await roleManager.CreateAsync(new(SD.CustomerRole));
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUserVM userVm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUsers identity = new ApplicationUsers()
                {
                    UserName = userVm.Name,
                    Email = userVm.Email,
                    City=userVm.City

                };
               
                var result = await userManager.CreateAsync(identity, userVm.Password);
                if (result.Succeeded)
                {
                    // Ok
                    // Assign role to user
                    
                    await userManager.AddToRoleAsync(identity, SD.CustomerRole);
                    await signInManager.SignInAsync(identity, false);
                    return RedirectToAction("Login", "Account");
                }

                ModelState.AddModelError("Password", "Invalid Password");
            }

            return View(userVm);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(loginVm userVm)
        {
            if (ModelState.IsValid)
            {
                var userDb = await userManager.FindByNameAsync(userVm.User);

                if (userDb != null)
                {
                    var finalResult = await userManager.CheckPasswordAsync(userDb, userVm.Password);

                    if (finalResult)
                    {
                        // Login ==> Create ID (cookies)
                        await signInManager.SignInAsync(userDb, userVm.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        // Error in password
                        ModelState.AddModelError("Password", "invalid passwrod");
                }
                else
                    // Error in userName
                    ModelState.AddModelError("User", "invalid UserName");

            }
            return View(userVm);
        }
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
