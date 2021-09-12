using FiorellaHomeWork.DAL;
using FiorellaHomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorellaHomeWork.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        public AppDbContext _context { get; }
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult  Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool hascategory = _context.categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (hascategory)
            {
                ModelState.AddModelError("Name", "ə qaqa bunnan vare");
                    return View();
            }
             _context.categories.Add(category);
             _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id==null)
                return NotFound();
                var category = await _context.categories.FirstOrDefaultAsync(c=>c.Id==id);
            
            if (category == null)
                return NotFound();

            
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int? id, Category category)
        {
            if (id == null)
                return NotFound();
            if (id != category.Id)
                return BadRequest();
            var categoryDb =  _context.categories
                 .FirstOrDefault(c => c.Id == id);
            bool hascategory = _context.categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (hascategory)
            {
                ModelState.AddModelError("Name", "ə qaqa bunnan vare");
                return View(categoryDb);
            }
            if (category == null)
                return NotFound();
            categoryDb.Name = category.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var slides =  _context.Slides.FirstOrDefault(sl => sl.Id == id);

            if (slides == null)
                return NotFound();


            return View(slides);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public  IActionResult DeletePost(int? id)
        {
            if (id == null)
                return NotFound();
            var slides =  _context.Slides.Find(id);
            if (slides == null)
                return NotFound();
               _context.Slides.Remove(slides);
              _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
