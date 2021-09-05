using FiorellaHomeWork.DAL;
using FiorellaHomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FiorellaHomeWork.Controllers
{
    public class BasketController : Controller
    {
        public AppDbContext _context { get; }
        public BasketController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var basket = JsonSerializer.Deserialize<List<Product>>(Request.Cookies["basket"]);

            return View();
        }
    }
}
