using FiorellaHomeWork.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorellaHomeWork.ViewComponents
{
    public class HeaderLogoViewComponent: ViewComponent
    {
        public AppDbContext _context { get; }
        public HeaderLogoViewComponent (AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _context.headerLogos;
            return View(await Task.FromResult(model));
        }

    }
}
