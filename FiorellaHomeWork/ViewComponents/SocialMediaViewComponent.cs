using FiorellaHomeWork.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorellaHomeWork.ViewComponents
{
    public class SocialMediaViewComponent : ViewComponent
    {
        public AppDbContext _context { get; }
        public SocialMediaViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =  _context.SocialMedias;
            return View(await Task.FromResult(model));
        }

    }
}
