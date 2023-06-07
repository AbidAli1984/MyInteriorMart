using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.BANNER
{
    [Table("Campaign", Schema = "banner")]
    public class Campaign
    {
        [Key]
        [Display(Name = "Campaign ID")]
        public int CampaignID { get; set; }

        [Display(Name = "Listing ID", Prompt = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "Owner GUID", Prompt = "Owner GUID")]
        [Required(ErrorMessage = "Owner GUID required.")]
        public string OwnerGUID { get; set; }

        [Display(Name = "Campaign Name", Prompt = "Owner GUID")]
        [Required(ErrorMessage = "Campaign Name required.")]
        public string CampaignName { get; set; }

        [Display(Name = "Date Created", Prompt = "Date Created")]
        [Required(ErrorMessage = "Date Created required.")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Start Date", Prompt = "Start Date")]
        [Required(ErrorMessage = "Start Date required.")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date", Prompt = "End Date")]
        [Required(ErrorMessage = "End Date required.")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Image Alt Text", Prompt = "Image Alt Text")]
        [Required(ErrorMessage = "Image Alt Text required.")]
        public string ImageAltText { get; set; }

        [Display(Name = "Destination URL", Prompt = "Destination URL")]
        [Required(ErrorMessage = "Destination URL required.")]
        public string DestinationURL { get; set; }

        [Display(Name = "Video URL", Prompt = "Video URL")]
        public string VideoURL { get; set; }

        [Display(Name = "HTML5 Banner Ad Script", Prompt = "Video URL")]
        [DataType(DataType.MultilineText)]
        public string HTML5BannerAdScript { get; set; }

        [Display(Name = "Banner Type", Prompt = "Banner Type")]
        [Required(ErrorMessage = "Banner Type required.")]
        public Nullable<int> BannerTypeID { get; set; }

        [Display(Name = "Banner Page", Prompt = "Banner Page")]
        [Required(ErrorMessage = "Banner Page required.")]
        public Nullable<int> BannerPageID { get; set; }

        [Display(Name = "Banner Space", Prompt = "Banner Space")]
        [Required(ErrorMessage = "Banner Space required.")]
        public Nullable<int> BannerSpaceID { get; set; }

        [Display(Name = "Impressions Count", Prompt = "Impressions Count")]
        [Required(ErrorMessage = "Impressions Count required.")]
        public int ImpressionsCount { get; set; }

        [Display(Name = "Clicks Count", Prompt = "Clicks Count")]
        [Required(ErrorMessage = "Clicks Count required.")]
        public int ClicksCount { get; set; }

        // Shafi: Navigation properties
        public virtual BannerType BannerType { get; set; }
        public virtual BannerPage BannerPage { get; set; }
        public virtual BannerSpace BannerSpace { get; set; }
        // End:
    }
}
