using EduhomeTemplate.Areas.Manage.ViewModels;
using EduhomeTemplate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class DashBoardController : Controller
    {
        private readonly UserManager<Appuser> _userManager;
        public DashBoardController(UserManager<Appuser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    Appuser appuser = new Appuser
        //    {
        //        UserName = "SuperAdmin",
        //        Fullname="Super Admin",
        //    };
        //    var result = await _userManager.CreateAsync(appuser, "Admin123");
        //    return Ok(result);
        //}
    }
}