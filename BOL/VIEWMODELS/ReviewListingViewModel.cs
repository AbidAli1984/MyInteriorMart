using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.VIEWMODELS
{
    public class ReviewListingViewModel
    {
        public int ReviewID { get; set; }
        public int ListingId { get; set; }
        public string OwnerGuid { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string NameFirstLetter { get; set; }
        public string ListingUrl { get; set; }
        public string BusinessCategory { get; set; }
        public string SecondCat { get; set; }
        public string Date { get; set; }
        public int Ratings { get; set; }
        public string Comment { get; set; }
        public string VisitTime { get; set; }
        public int RatingLimit { get { return 5; } }
        public string CreatedDate { get; set; }
    }
}
