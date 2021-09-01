using FiorellaHomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorellaHomeWork.ViewModels
{
    public class HomeViewModel
    {
        public List<Slide> Slides { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public Introduction Introduction { get; set; }
    }
}
