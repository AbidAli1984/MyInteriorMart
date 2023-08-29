using BOL.BANNERADS;
using BOL.CATEGORIES;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.LISTING;
using BOL.LISTING.UploadImage;
using BOL.VIEWMODELS;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.Listings
{
    public class ListingDetailVM
    {
        public int ListingId { get; set; }
        public string LogoUrl { get; set; }
        public LogoImage LogoImage { get; set; } = new LogoImage();
        public IList<OwnerImageVM> OwnerImagesVM { get; set; } = new List<OwnerImageVM>();
        public IList<GalleryImage> GalleryImages { get; set; } = new List<GalleryImage>();
        public string CurrentUserId { get; set; }
        public Listing Listing { get; set; } = new Listing();
        public Communication Communication { get; set; } = new Communication();
        public AddressVM Address { get; set; } = new AddressVM();
        public BusinessWorkingHour BusinessWorkingHour { get; set; } = new BusinessWorkingHour();
        public Specialisation Specialisation { get; set; } = new Specialisation();
        public List<Keyword> Keywords { get; set; } = new List<Keyword>();
        public WorkingHours WorkingHour { get; set; } = new WorkingHours();
        public PaymentMode PaymentMode { get; set; } = new PaymentMode();

        public BannerDetail BannerImageDetail { get; set; } = new BannerDetail();
        public IList<ReviewListingViewModel> listReviews = new List<ReviewListingViewModel>();
        public IList<ImageDetails> CertificateImages { get; set; } = new List<ImageDetails>();
        public IList<ImageDetails> ClientImages { get; set; } = new List<ImageDetails>();
        public Rating CurrentUserRating { get; set; } = new Rating();
        public decimal Rating { 
            get
            {
                if (CurrentUserRating == null)
                {
                    CurrentUserRating = new Rating();
                    return 0;
                }
                return CurrentUserRating.Ratings;
            }
            set
            {
                int.TryParse(value.ToString(), out int rating);
                CurrentUserRating.Ratings = rating;
            }
        }

        public bool IsSubscribe { get; set; } = false;
        public bool IsBookmarked { get; set; } = false;
        public bool IsLiked { get; set; } = false;
        public int RatingCount { get; set; }
        public decimal RatingAverage { get; set; }

        public string shareUrl { get; set; }
    }
}
