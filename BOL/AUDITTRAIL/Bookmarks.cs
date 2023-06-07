using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.AUDITTRAIL
{
    [Table("Bookmarks", Schema = "audit")]
    public class Bookmarks
    {
        [Key]
        [Display(Name = "BookmarksID")]
        public int BookmarksID { get; set; }

        [Display(Name = "Listing ID")]
        public int ListingID { get; set; }

        [Display(Name = "User Guid", Prompt = "User Guid")]
        [Required(ErrorMessage = "User Guid required.")]
        public string UserGuid { get; set; }

        [Display(Name = "Email", Prompt = "Email")]
        public string Email { get; set; }

        [Display(Name = "Mobile", Prompt = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }

        [Display(Name = "User Role")]
        public string UserRole { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }

        [Display(Name = "Time")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VisitTime { get; set; }

        [Display(Name = "User Agent")]
        public string UserAgent { get; set; }

        [Display(Name = "Bookmark Status")]
        public bool Bookmark { get; set; }
    }
}
