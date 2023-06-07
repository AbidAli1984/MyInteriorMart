using System;
using System.Collections.Generic;
using System.Text;
using BOL.LISTING;
using BOL.VIEWMODELS;

namespace BOL.VIEWMODELS
{
    public class FreeListingViewModel
    {
        public Listing Listing { get; set; }
        public Communication Communication { get; set; }
        public ListingAddressVM Address { get; set; }
        public ListingCategoryVM Category { get; set; }
        public Specialisation Specialisation { get; set; }
        public WorkingHours WorkingHour { get; set; }
        public PaymentMode PaymentMode { get; set; }

        // Begin: Listing Open or Close
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string OpenOn { get; set; }
        public bool Closed { get; set; }
        // End:

        // Begin: Rating properties
        public string ratingAverage { get; set; }
        public string rating1 { get; set; }
        public string rating2 { get; set; }
        public string rating3 { get; set; }
        public string rating4 { get; set; }
        public string rating5 { get; set; }
        // End:
    }
}
