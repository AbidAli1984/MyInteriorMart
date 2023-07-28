﻿using BOL.BANNERADS;
using BOL.CATEGORIES;
using BOL.LISTING;
using BOL.VIEWMODELS;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.Listings
{
    public class ListingDetailVM
    {
        public string LogoUrl { get; set; }
        public string CurrentUserId { get; set; }
        public Listing Listing { get; set; } = new Listing();
        public Communication Communication { get; set; } = new Communication();
        public AddressVM Address { get; set; } = new AddressVM();
        public BusinessWorkingHour BusinessWorkingHour { get; set; } = new BusinessWorkingHour();
        public string FirstCategory { get; set; }
        public string SecondCategory { get; set; }

        public Specialisation Specialisation { get; set; } = new Specialisation();
        public WorkingHours WorkingHour { get; set; } = new WorkingHours();
        public PaymentMode PaymentMode { get; set; } = new PaymentMode();

        public IEnumerable<ListingBanner> ListingBanners { get; set; } = new List<ListingBanner>();
        public IList<ReviewListingViewModel> listReviews = new List<ReviewListingViewModel>();
        public Rating CurrentUserRating { get; set; } = new Rating();

        public bool UserAlreadySubscribed { get; set; } = false;
        public bool UserAlreadyBookmarked { get; set; } = false;
        public bool UserAlreadyLiked { get; set; } = false;
        public int RatingCount { get; set; }
        public decimal RatingAverage { get; set; }
        public int TotalBookmark { get; set; }
        public int TotalSubscriber { get; set; }
        public int TotalLikes { get; set; }
    }
}