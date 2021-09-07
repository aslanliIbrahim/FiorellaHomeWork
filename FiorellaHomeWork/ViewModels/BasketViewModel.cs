using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorellaHomeWork.ViewModels
{
    public class BasketViewModel
    {
        public List<BasketItemViewModel> basketItemViewModels { get; set; }
        public decimal TotalPrice { get; set; }
        public int Count { get; set; }
    }
}
