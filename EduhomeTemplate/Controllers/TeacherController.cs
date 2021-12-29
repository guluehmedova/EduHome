using EduhomeTemplate.Models;
using EduhomeTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduhomeTemplate.Controllers
{
    public class TeacherController : Controller
    {
        private DataContext _context;
        public TeacherController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TeacherViewModel teacherViewModel = new TeacherViewModel
            {
                Teachers = _context.Teachers.ToList(),
            };
            return View(teacherViewModel);
        }
        public IActionResult Detail(int id)
        {
            Teacher teacher = _context.Teachers.FirstOrDefault(x => x.Id == id);
            return View();
        }
    }
}
