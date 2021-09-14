using FiorellaHomeWork.Areas.AdminPanel.ViewModels;
using FiorellaHomeWork.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorellaHomeWork.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
             _userManager = userManager;
            _roleManager = _roleManager;
        }
        public IActionResult Register()
        {
            AdminRegisterVM vm = new AdminRegisterVM();
            vm.Roles = new List<RoleVM>();
            foreach (var role in Enum.GetNames(typeof(Helper.Help.Roles)))
            {
                vm.Roles.Add(new RoleVM { Name = role });
            }
            return View(vm);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(AdminRegisterVM register)
        {
            AdminRegisterVM vm = new AdminRegisterVM();

            vm.Roles = new List<RoleVM>();
            foreach (var role in Enum.GetNames(typeof(Helper.Help.Roles)))
            {
                vm.Roles.Add(new RoleVM { Name = role });
            }
            if (!ModelState.IsValid) return View(vm);

            AppUser user = new AppUser
            {
                UserName = register.Username,
                Email = register.Email
            };
            
            IdentityResult result  = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                    return View(vm);
                }
            }
            await _userManager.AddToRoleAsync(user, register.Role);
            return RedirectToAction("Index","Home", new { area = "AdminPanel" });
        }

        //public async Task CreateRoles()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole(Helper.Help.Roles.SuperAdmin.ToString()));
        //    await _roleManager.CreateAsync(new IdentityRole(Helper.Help.Roles.Admin.ToString()));
        //    await _roleManager.CreateAsync(new IdentityRole(Helper.Help.Roles.Member.ToString()));

        //}
    }
}
