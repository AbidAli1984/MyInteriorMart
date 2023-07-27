using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.Listings
{
    public class ListingResultVM
    {
        public int ListingId { get; set; }
        public string CompanyName { get; set; }
        public string SubCategory { get; set; }
        public string Assembly { get; set; }
        public string Area { get; set; }
        public string Mobile { get; set; }
        public decimal RatingAverage { get; set; }
        public int RatingCount { get; set; }
        public int BusinessYear { get; set; }
        public BusinessWorkingHour BusinessWorking { get; set; } = new BusinessWorkingHour();


        public string Url { get; set; }
        public int SubCategoryId { get; set; }
        public string Email { get; set; }
        public bool Claimed { get; set; }
    }
}
