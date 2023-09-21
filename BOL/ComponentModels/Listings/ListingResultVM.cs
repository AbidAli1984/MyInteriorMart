using BOL.BANNERADS;
using BOL.LISTING.UploadImage;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.Listings
{
    public class ListingResultVM
    {
        public int ListingId { get; set; }
        public string id { get; set; }
        public string CompanyName { get; set; }
        public string SubCategory { get; set; }
        public string ListingUrl { get; set; }
        public string City { get; set; }
        public string Locality { get; set; }
        public string Area { get; set; }
        public string Mobile { get; set; }
        public decimal RatingAverage { get; set; }
        public int RatingCount { get; set; }
        public int BusinessYear { get; set; }
        public BusinessWorkingHour BusinessWorking { get; set; } = new BusinessWorkingHour();
        public LogoImage LogoImage { get; set; } = new LogoImage();
        public string LogoImageUrl { 
            get
            {
                return LogoImage == null || string.IsNullOrWhiteSpace(LogoImage.ImagePath) ? "/resources/img/hotel_2.jpg" : LogoImage.ImagePath;
            }
        }

        public string Url { get; set; }
        public string Email { get; set; }
    }

    public class ListingResultBannerVM
    {
        public IEnumerable<CategoryBanner> CategoryBannersTop { get; set; }
        public IEnumerable<CategoryBanner> CategoryBannersLeftTop { get; set; }
        public IEnumerable<CategoryBanner> CategoryBannersLeftBottom { get; set; }
    }
}
