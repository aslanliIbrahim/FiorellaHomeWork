using FiorellaHomeWork.DAL;
using FiorellaHomeWork.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FiorellaHomeWork.Controllers
{
    [Area("AdminPanel")]
    public class SlidesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SlidesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.Slides);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slide slide)
        {
            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();
            foreach (var ph in slide.Photos)
            {
                if (!ph.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("Photo", "bu sekli secdee ala");
                    return View();
                }
                if (ph.Length / 1024 > 200)
                {
                    ModelState.AddModelError("Photo", "adam kimi sey secde alaa");
                    return View();
                }
                string filename = Guid.NewGuid().ToString() + ph.FileName;
                string resultpath = Path.Combine(_env.WebRootPath, "img", filename);
                using (FileStream fileStream = new FileStream(resultpath, FileMode.Create))
                {
                    await ph.CopyToAsync(fileStream);
                }
                Slide newSlide = new Slide
                {
                    Image = filename
                };
                _context.Add(newSlide);
                await _context.SaveChangesAsync();
            } 
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var slider = _context.Slides.FirstOrDefault(s => s.Id == id);
            if (slider == null)
            {
                return BadRequest();
            }


            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public  ActionResult DeleteImage(int? id)
        {
            if (id == null)
                return NotFound();
            var slide =  _context.Slides.Find(id);
            if (slide == null)
                return NotFound();
            string resultpath = Path.Combine(_env.WebRootPath, "img", slide.Image);
            if (System.IO.File.Exists(resultpath))
            {
                System.IO.File.Delete(resultpath);
            }
            var slider = _context.Slides.FirstOrDefault(s => s.Id == id);
            if (slider == null)
            {
                return BadRequest();
            }
            else if (id != slider.Id)
            {
                return BadRequest();
            }

            string environment = _env.WebRootPath;
            string folderPath = Path.Combine(environment, "img", slide.Image);
            FileInfo file = new FileInfo(folderPath);
            file.Delete();
            _context.Slides.Remove(slide);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            //var photoName = "";
            //photoName = photoFileName + ".jpg";
            //string fullPath =Path.Combine("~/img" + photoName);
        }
    }
}
