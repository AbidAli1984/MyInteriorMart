using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.VIEWMODELS
{
    public class SubscribeListingViewModel
    {
        public int SubscribeID { get; set; }
        public string UserGuid { get; set; }
        public int ListingID { get; set; }
        public string CompanyName { get; set; }
        public string VisitDate { get; set; }
        public string VisitTime { get; set; }
        public string NameFirstLetter { get; set; }
        public string ListingUrl { get; set; }
        public string FirstCat { get; set; }
        public string SecondCat { get; set; }
    }
}
