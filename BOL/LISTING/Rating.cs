using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LISTING
{
    [Table("Rating", Schema = "listing")]
    public class Rating
    {
        [Key]
        [Display(Name = "Rating ID")]
        public int RatingID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        // Shafi: Date and time
        [Display(Name = "Date")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Time")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        public DateTime Time { get; set; }

        [Display(Name = "Rating")]
        [Required(ErrorMessage = "Rating required.")]
        public int Ratings { get; set; }

        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Comment Required")]
        public string Comment { get; set; }

        [Display(Name = "Approved By Admin")]
        public bool ApprovedByAdmin { get; set; }
    }
}
