using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LISTING
{
    [Table("ListingViewCount", Schema = "listing")]
    public class ListingViewCount
    {
        [Key]
        [Display(Name = "View Count ID")]
        public int ViewCountID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "View Count")]
        public int ViewCount { get; set; }

        [Display(Name = "Impression Count")]
        public int ImpressionCount { get; set; }
    }
}
