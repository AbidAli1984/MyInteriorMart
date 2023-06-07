using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.LISTING
{
    [Table("Profile", Schema = "listing")]
    public class Profile
    {
        [Key]
        [Display(Name = "Profile ID")]
        public int ProfileID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "Owner Guid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        [Display(Name = "Profile Details", Prompt = "About company, services etc.")]
        [MaxLength(10000, ErrorMessage = "Maximum 10000 characters allowed.")]
        [MinLength(5, ErrorMessage = "Minimum 5 characters rerquired.")]
        [Required(ErrorMessage = "Profile details required.")]
        [DataType(DataType.MultilineText)]
        public string ProfileDetails { get; set; }
    }
}
