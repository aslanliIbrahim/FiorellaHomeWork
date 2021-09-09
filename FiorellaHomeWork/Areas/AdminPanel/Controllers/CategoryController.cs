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
    }
}
