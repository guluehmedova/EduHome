using EduhomeTemplate.Models;
using EduhomeTemplate.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduhomeTemplate.Controllers
{
    public class AccountController : Controller
    {
        private DataContext _context;
        private UserManager<Appuser> _userManager;
        private SignInManager<Appuser> _signInManager;
        public AccountController(DataContext context, UserManager<Appuser> userManager, SignInManager<Appuser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(MemberRegisterViewModel memberRegisterViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Appuser user = await _userManager.FindByNameAsync(memberRegisterViewModel.Username);
            if (user != null)
            {
                ModelState.AddModelError("Username", "This user is already exsist!");
                return View();
            }
            if (_context.Users.Any(x=>x.NormalizedEmail==memberRegisterViewModel.Email.ToUpper()))
            {
                ModelState.AddModelError("Email", "Email is already exsist!");
                return View();
            }
            user = new Appuser
            {
                UserName = memberRegisterViewModel.Username,
                Fullname=memberRegisterViewModel.Fullname,
                Email=memberRegisterViewModel.Email
            };
            var result = await _userManager.CreateAsync(user, memberRegisterViewModel.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
           return RedirectToAction("index", "home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MemberRegisterViewModel memberRegisterViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Appuser user = await _userManager.FindByNameAsync(memberRegisterViewModel.Username);

            if (user==null)
            {
                ModelState.AddModelError("", "password or username are uncorrect");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, memberRegisterViewModel.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "password or username are not correct");
                return View();
            }

            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Profile()
        {
            Appuser user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }
    }
}
