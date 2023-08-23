using BOL.LISTING;
using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace BOL.ComponentModels.Listings
{
    public class ListingEnquiryVM
    {
        public ListingEnquiry ListingEnquiry { get; set; } = new ListingEnquiry();

        public bool isValid()
        {
            return !string.IsNullOrEmpty(ListingEnquiry.FullName) && !string.IsNullOrEmpty(ListingEnquiry.MobileNumber) &&
                !string.IsNullOrEmpty(ListingEnquiry.EnquiryTitle) && !string.IsNullOrEmpty(ListingEnquiry.Email) &&
                !string.IsNullOrEmpty(ListingEnquiry.Message);
        }
    }
}
