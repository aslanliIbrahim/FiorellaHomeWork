using FiorellaHomeWork.DAL;
using FiorellaHomeWork.Models;
using FiorellaHomeWork.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorellaHomeWork.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public HeaderViewComponent(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string basket = HttpContext.Request.Cookies["basket"];

            BasketViewModel basketView = new BasketViewModel
            {
                basketItemViewModels = new List<BasketItemViewModel>(),
                TotalPrice = 0,
                Count = 0
            };

            if (basket != null)
            {
                List<CookieItemViewModel> cookieItemViewModels = JsonConvert.DeserializeObject<List<CookieItemViewModel>>(basket);
                foreach (var item in cookieItemViewModels)
                {
                    Product product = _context.Products.FirstOrDefault(p => p.Id == item.Id);
                    if (product != null)
                    {

                        BasketItemViewModel basketItemViewModel = new BasketItemViewModel
                        {
                            Product = product,
                            Count = item.Count
                        };
                        basketView.basketItemViewModels.Add(basketItemViewModel);
                        basketView.TotalPrice +=(decimal)(item.Count * product.Price);
                        basketView.Count++;
                    }
                }
            }
            return View(await Task.FromResult(basketView));
        }
    }
}
