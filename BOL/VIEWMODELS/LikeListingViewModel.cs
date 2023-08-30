using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.VIEWMODELS
{
    public class LikeListingViewModel
    {
        public int LikeDislikeID { get; set; }
        public int ListingID { get; set; }
        public string CompanyName { get; set; }
        public string VisitDate { get; set; }
        public string VisitTime { get; set; }
        public string OwnerGuid { get; set; }
        public string Name { get; set; }
        public string NameFirstLetter { get; set; }
        public string ListingUrl { get; set; }
        public string BusinessCategory { get; set; }
        public string SecondCat { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
    }
}
