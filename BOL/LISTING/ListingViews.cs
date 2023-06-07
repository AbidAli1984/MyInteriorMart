using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LISTING
{
    [Table("ListingViews", Schema = "listing")]
    public class ListingViews
    {
        [Key]
        [Display(Name = "Listing View ID")]
        public int ListingViewID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "User Type", Prompt = "UserType")]
        [Required(ErrorMessage = "User Type required.")]
        public string UserType { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        // Shafi: Date and time
        [Display(Name = "Date")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Pincode")]
        public string Pincode { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "IPv4")]
        public string IPv4 { get; set; }

        [Display(Name = "latitude")]
        public string Latitude { get; set; }

        [Display(Name = "longitude")]
        public string Longitude { get; set; }
    }
}
