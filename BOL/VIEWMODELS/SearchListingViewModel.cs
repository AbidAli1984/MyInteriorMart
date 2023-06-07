using BOL.LISTING;

namespace BOL.VIEWMODELS
{
    public class SearchListingViewModel
    {
        public Listing Listing { get; set; }
        public Communication Communication { get; set; }
        public Address Address { get; set; }
        public Categories Categories { get; set; }
        public Specialisation Specialisation { get; set; }
        public WorkingHours WorkingHours { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public ListingViewCount ListingViewCount { get; set; }
    }
}
