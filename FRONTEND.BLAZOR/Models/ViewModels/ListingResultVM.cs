using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Models.ViewModels
{
    public class ListingResultVM
    {
        public int ListingId { get; set; }
        public string CompanyName { get; set; }
        public string Url { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategory { get; set; }
        public string Assembly { get; set; }
        public string Area { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string OpenClose { get; set; }
        public bool Claimed { get; set; }
        public string RatingAverage { get; set; }
        public int RatingCount { get; set; }
    }
}
