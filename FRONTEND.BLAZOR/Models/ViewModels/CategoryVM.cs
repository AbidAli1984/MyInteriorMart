using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Models.ViewModels
{
    public class CategoryVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool HasChild { get; set; }
    }
}
