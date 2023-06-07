using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LABOURNAKA
{
    [Table("Contact", Schema = "lab")]
    public class Contact
    {
        [Key]
        [Display(Name = "Contact Id")]
        public int ContactId { get; set; }

        [Display(Name = "User Guid")]
        [Required(ErrorMessage = "User Guid Required")]
        public string UserGuid { get; set; }

        [Display(Name = "Permanent Address")]
        [Required(ErrorMessage = "Permanent Address Required")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Native Address")]
        [Required(ErrorMessage = "Native Address Required")]
        public string NativeAddress { get; set; }

        [Display(Name = "WhatsApp Mobile")]
        [Required(ErrorMessage = "WhatsApp Mobile Required")]
        public string WhatsAppMobile { get; set; }

        [Display(Name = "Alternate Mobile")]
        public string AlternateMobile { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
