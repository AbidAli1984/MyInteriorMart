using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace BOL.LISTING
{
    [Table("SocialNetwork")]
    public class SocialNetwork
    {
        [Key]
        [Display(Name = "SocialNetwork ID")]
        public int SocialNetworkID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        [Display(Name = "Whatsapp Group Link", Prompt = "https://chat.whatsapp.com/Group-Name")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters required.")]
        [MaxLength(500, ErrorMessage = "Maximum 500 characters allowed.")]
        public string WhatsappGroupLink { get; set; }

        [Display(Name = "Youtube", Prompt = "https://www.youtube.com/Channel-Name")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters required.")]
        [MaxLength(500, ErrorMessage = "Maximum 500 characters allowed.")]
        public string Youtube { get; set; }

        [Display(Name = "Facebook", Prompt = "https://www.facebook.com/Company-Name")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters required.")]
        [MaxLength(500, ErrorMessage = "Maximum 500 characters allowed.")]
        public string Facebook { get; set; }

        [Display(Name = "Instagram", Prompt = "https://www.instagram.com/Company-Name")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters required.")]
        [MaxLength(500, ErrorMessage = "Maximum 500 characters allowed.")]
        public string Instagram { get; set; }

        [Display(Name = "Linkedin", Prompt = "https://in.linkedin.com/in/Company-Name")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters required.")]
        [MaxLength(500, ErrorMessage = "Maximum 500 characters allowed.")]
        public string Linkedin { get; set; }

        [Display(Name = "Pinterest", Prompt = "https://www.pinterest.com/Company-Name")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters required.")]
        [MaxLength(500, ErrorMessage = "Maximum 500 characters allowed.")]
        public string Pinterest { get; set; }

        [Display(Name = "Twitter", Prompt = "https://t.me/Group-Name")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters required.")]
        [MaxLength(500, ErrorMessage = "Maximum 500 characters allowed.")]
        public string Twitter { get; set; }
    }
}
