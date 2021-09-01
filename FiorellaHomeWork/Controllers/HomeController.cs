using FiorellaHomeWork.DAL;
using FiorellaHomeWork.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorellaHomeWork.Controllers
{
    public class HomeController : Controller
    {
        public AppDbContext _context { get; }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeViewModel homevm = new HomeViewModel
            {
                Slides = await _context.Slides.ToListAsync(),
                Introduction = await _context.Introduction.FirstOrDefaultAsync(),
                Categories = await _context.categories.Where(c=>c.IsDeleted==false).ToListAsync(),
                Products = await _context.Products.Include(p=>p.Images).Include(p=>p.Category).ToListAsync()
            };

            return View(homevm);
        }
    }
}
