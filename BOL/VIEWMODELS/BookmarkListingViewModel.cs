using System;
using System.Collections.Generic;
using System.Text;
using BOL.LISTING;
using BOL.AUDITTRAIL;

namespace BOL.VIEWMODELS
{
    public class BookmarkListingViewModel
    {
        public int BookmarkID { get; set; }
        public string UserGuid { get; set; }
        public int ListingID { get; set; }
        public string CompanyName { get; set; }
        public string VisitDate { get; set; }
        public string VisitTime { get; set; }
        public string NameFirstLetter { get; set; }
        public string ListingUrl { get; set; }
        public string FirstCat { get; set; }
        public string SecondCat { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
    }
}
