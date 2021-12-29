using EduhomeTemplate.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduhomeTemplate.Areas.Manage.Controllers
{
    [Area("manage")]
    public class TeacherController : Controller
    {
        private DataContext _datacontext;
        private readonly IWebHostEnvironment _env;
        public TeacherController(DataContext dataContext, IWebHostEnvironment env)
        {
            _datacontext = dataContext;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_datacontext.Teachers.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher)
        {
            if (teacher.ImageFile == null)
                ModelState.AddModelError("ImageFile", "ImageFile is required");
            else if (teacher.ImageFile.ToString().Length > 2097152)
                ModelState.AddModelError("ImageFile", "ImageFile max size is 2MB");
            else if (teacher.ImageFile.ContentType != "image/jpeg" && teacher.ImageFile.ContentType != "image/png")
                ModelState.AddModelError("ImageFile", "ContentType must be image/jpeg or image/png");
            if (!ModelState.IsValid) return View();
            string filename = teacher.ImageFile.FileName.Length <= 64 ? teacher.ImageFile.FileName : (teacher.ImageFile.FileName.Substring(teacher.ImageFile.FileName.Length - 64, 64));
            filename = Guid.NewGuid().ToString() + filename;
            string path = Path.Combine(_env.WebRootPath, "uploads/teachers", filename);
            using (FileStream stream = new FileStream(path,FileMode.Create))
            {
                teacher.ImageFile.CopyTo(stream);
            }

            teacher.Image = filename;
            _datacontext.Teachers.Add(teacher);
            _datacontext.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            Teacher teacher = _datacontext.Teachers.FirstOrDefault(x => x.Id == id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Teacher exictsteacher = _datacontext.Teachers.FirstOrDefault(x => x.Id == teacher.Id);
            if (exictsteacher == null)
            {
                return NotFound();
            }
            if (teacher.ImageFile != null)
            {
                if (teacher.ImageFile.ToString().Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "ImageFile max size is 2MB");
                }
                if (teacher.ImageFile.ContentType != "image/jpeg" && teacher.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "ContentType must be image/jpeg or image/png");
                }
                string filename = teacher.ImageFile.FileName.Length <= 64 ? teacher.ImageFile.FileName : (teacher.ImageFile.FileName.Substring(teacher.ImageFile.FileName.Length - 64, 64));
                filename = Guid.NewGuid().ToString() + filename;
                string path = Path.Combine(_env.WebRootPath, "uploads/teachers", exictsteacher.Image);

                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                path = Path.Combine(_env.WebRootPath, "uploads/teachers", teacher.ImageFile.FileName);
                using (FileStream stream = new FileStream(path,FileMode.Create))
                {
                    teacher.ImageFile.CopyTo(stream);
                }
                exictsteacher.Image = teacher.ImageFile.FileName;
            }
            exictsteacher.AboutTitle = teacher.AboutTitle;
            exictsteacher.Degree = teacher.Degree;
            exictsteacher.Desc = teacher.Desc;
            exictsteacher.Experience = teacher.Experience;
            exictsteacher.Faculty = teacher.Faculty;
            exictsteacher.Hobbies = teacher.Hobbies;
            _datacontext.SaveChanges();
            return RedirectToAction("index", "teacher");
        }
        public IActionResult Delete(int id)
        {
            Teacher teacher = _datacontext.Teachers.FirstOrDefault(x => x.Id == id);

            if (teacher == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(teacher.Image))
            {
                string path = Path.Combine(_env.WebRootPath, "uploads/teachers", teacher.Image);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            _datacontext.Teachers.Remove(teacher);
            _datacontext.SaveChanges();

            return Ok();
        }
    }
}
