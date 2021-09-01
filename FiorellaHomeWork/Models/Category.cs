using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorellaHomeWork.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedTime { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
