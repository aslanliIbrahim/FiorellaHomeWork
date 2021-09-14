using FiorellaHomeWork.Models;
using FiorellaHomeWork.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorellaHomeWork.Controllers
{
    public class AcoountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _SignInManager;

        public AcoountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _SignInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);

            AppUser user = new AppUser
            {
                UserName = register.UserName,
                FullName = register.FullName,
                Email = register.YourEmail

            };

            IdentityResult result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description); 
                }
                return View(register);

            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View(login);
            AppUser user = await _userManager.FindByNameAsync(login.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "you have an error");
                return View(login);
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _SignInManager.PasswordSignInAsync(user,login.Password,false,false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "you have an error");
                return View(login);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }
    }
}
