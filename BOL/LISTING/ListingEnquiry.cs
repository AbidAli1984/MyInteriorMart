using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LISTING
{
    [Table("Enquiry")]
    public class ListingEnquiry
    {
        [Key]
        [Display(Name = "Enquiry ID")]
        public int Id { get; set; }

        [Display(Name = "Listing ID")]
        public int ListingID { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        public string OwnerGuid { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name Required")]
        public string FullName { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Mobile Number Required")]
        public string MobileNumber { get; set; }

        [Display(Name = "Enquiry Title")]
        [Required(ErrorMessage = "Enquiry Title Required")]
        public string EnquiryTitle { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string DateFormatted { get { return CreatedDate.ToString(Constants.dateFormat1); } }
    }
}
