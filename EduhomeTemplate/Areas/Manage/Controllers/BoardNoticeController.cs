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
        public IActionResult Index(int page)
        {
            ViewBag.TotalPage = (int)Math.Ceiling(Convert.ToDouble(_datacontext.Boards.Count()) / 4);
            ViewBag.SelectedPage = page;
            return View(_datacontext.Boards.Skip((page - 1) * 4).Take(8).ToList());
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
            Board board = _datacontext.Boards.FirstOrDefault(x => x.Id == id);

            if (board == null) return NotFound();

            return View(board);
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
        public IActionResult Delete(int id)
        {
            Board board = _datacontext.Boards.FirstOrDefault(x => x.Id == id);
            if (board == null) return NotFound();
            return View(board);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Board board)
        {
            Board existboard = _datacontext.Boards.FirstOrDefault(x => x.Id == board.Id);
            if (existboard == null) return NotFound();
            _datacontext.Boards.Remove(existboard);
            _datacontext.SaveChanges();
            return RedirectToAction("index", "boardnotice");
        }
    }
}
