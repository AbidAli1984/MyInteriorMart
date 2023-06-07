using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BOL.AUDITTRAIL
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name required.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Mobile required.")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "Message required.")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City required.")]
        public string City { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date required.")]
        public DateTime Date { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address required.")]
        public string IPAddress { get; set; }
    }
}
