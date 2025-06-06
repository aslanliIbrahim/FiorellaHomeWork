﻿using FiorellaHomeWork.DAL;
using FiorellaHomeWork.Models;
using FiorellaHomeWork.ViewComponents;
using FiorellaHomeWork.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
                Products = await _context.Products.Include(p=>p.Images).Include(p=>p.Category).OrderByDescending(p=>p.Id).
                Take(8).ToListAsync()
            };
 
            //HttpContext.Session.SetString("name", "ibrahim");
            //List<AddBasket> baskets = new List<AddBasket>
            //{
            //    new AddBasket{Id=1, Count=2},
            //    new AddBasket{Id=2, Count=4},
            //    new AddBasket{Id=3, Count=5}
            //};
            //Response.Cookies.Append("basket", JsonSerializer.Serialize(baskets));
            return View(homevm);
        }

        public IActionResult AddBasket(int id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            string basket = HttpContext.Request.Cookies["basket"];
            if (basket == null)
            {
                List<CookieItemViewModel> products = new List<CookieItemViewModel>();
                products.Add(new CookieItemViewModel
                {
                    Id  = product.Id,
                    Count = 1
                });
                string basketStr = JsonConvert.SerializeObject(products);
                HttpContext.Response.Cookies.Append("basket", basketStr);
            }
            else
            { 
                List<CookieItemViewModel> products = JsonConvert.DeserializeObject<List<CookieItemViewModel>>(basket);
                CookieItemViewModel cookieItem = products.FirstOrDefault(p => p.Id == product.Id);
                if (cookieItem == null)
                {
                    products.Add(new CookieItemViewModel
                    {
                        Id = product.Id,
                        Count = 1
                    });
                }
                else
                {
                    cookieItem.Count++;
                }
                string basketStr = JsonConvert.SerializeObject(products);
                HttpContext.Response.Cookies.Append("basket", basketStr);
            }

           
            return RedirectToAction("Index", "Home");
            //string sesionData = HttpContext.Session.GetString("name");
            //List<AddBasket> cookieData =
            //    JsonSerializer.Deserialize<List<AddBasket>>(Request.Cookies["basket"]);
            //return View(cookieData);/*Json(cookieData);*/ /*RedirectToAction(nameof(Index));*/
        }
        public IActionResult ShowBasket()
        {
            return Content(HttpContext.Request.Cookies["basket"]);
        }

        public ActionResult RemoveItem(int id)
        {
            string basket = HttpContext.Request.Cookies["basket"];
            List<CookieItemViewModel> products = JsonConvert.DeserializeObject<List<CookieItemViewModel>>(basket);
            foreach (var item in products)
            {
                if (item.Id == id)
                {
                    products.Remove(item);
                    break;
                }
            }
            string basketStr = JsonConvert.SerializeObject(products);
            HttpContext.Response.Cookies.Append("basket", basketStr);
            return RedirectToAction("Index","Home");
        }
    }
}
