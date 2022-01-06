using EduhomeTemplate.Areas.Manage.ViewModels;
using EduhomeTemplate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduhomeTemplate.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private UserManager<Appuser> _userManager;
        private SignInManager<Appuser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<Appuser> userManager, SignInManager<Appuser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Login()
        {
            Appuser user = _userManager.FindByNameAsync("SuperAdmin").Result;
            var result = _userManager.AddToRoleAsync(user, "SuperAdmin").Result;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginView)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Appuser admin = await _userManager.FindByNameAsync(adminLoginView.UserName);
            if (admin == null)
            {
                ModelState.AddModelError("", "Password or username incorrect");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(admin, adminLoginView.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Password or username incorrect");
                return View();
            }
            return RedirectToAction("index", "dashboard");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }
        public async Task<IActionResult> CreateRole()
        {
            IdentityRole r1 = new IdentityRole("SuperAdmin");
            IdentityRole r2 = new IdentityRole("Admin");
            IdentityRole r3 = new IdentityRole("Member");
            await _roleManager.CreateAsync(r1);
            await _roleManager.CreateAsync(r2);
            await _roleManager.CreateAsync(r3);
            return Ok();
        }
    }
}
