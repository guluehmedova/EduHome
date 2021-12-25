using EduhomeTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduhomeTemplate.Areas.Manage.Controllers
{
    [Area("manage")]
    public class BoardNoticeController : Controller
    {
        private DataContext _datacontext;
        public BoardNoticeController(DataContext dataContext)
        {
            _datacontext = dataContext;
        }
        public IActionResult Index()
        {
            List<Board> notices = _datacontext.Boards.ToList();
            return View(notices);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Board board)
        {
            _datacontext.Boards.Add(board);
            _datacontext.SaveChanges();
            return RedirectToAction("index", "boardnotice");
        }
        public IActionResult Edit(int id)
        {
            Board genre = _datacontext.Boards.FirstOrDefault(x => x.Id == id);

            if (genre == null) return NotFound();

            return View(genre);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Board board)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Board exictsboard = _datacontext.Boards.FirstOrDefault(x => x.Id == board.Id);
            if (exictsboard == null)
            {
                return NotFound();
            }
            exictsboard.Title = board.Title;
            exictsboard.Time = board.Time;
            exictsboard.Desc = board.Desc;

            _datacontext.SaveChanges();

            return RedirectToAction("index", "boardnotice");
        }
    }
}
