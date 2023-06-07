using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.LISTING
{
    [Table("Communication", Schema = "listing")]
    public class Communication
    {
        [Key]
        [Display(Name = "Communication ID")]
        public int CommunicationID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        [Display(Name = "Email", Prompt = "user@xyz.com etc")]
        [MaxLength(200, ErrorMessage = "Maximum 200 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Email required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Valid email address required.")]
        public string Email { get; set; }

        [Display(Name = "Website", Prompt = "https://www.xyz.com")]
        [MaxLength(1000, ErrorMessage = "Maximum 1000 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [DataType(DataType.Url, ErrorMessage = "URL must be in https://www.xyz.com format")]
        public string Website { get; set; }

        [Display(Name = "Mobile", Prompt = "9587858485 etc.")]
        [Required(ErrorMessage = "10 digit mobile number required.")]
        [MaxLength(15, ErrorMessage = "Maximum 15 digits are allowed.")]
        [MinLength(10, ErrorMessage = "Minimum 10 digits required.")]
        public string Mobile { get; set; }

        [Display(Name = "Whatsapp", Prompt = "9587858485 etc.")]
        [Required(ErrorMessage = "10 digit whatsapp number required.")]
        [MaxLength(15, ErrorMessage = "Maximum 15 digits are allowed.")]
        [MinLength(10, ErrorMessage = "Minimum 10 digits required.")]
        public string Whatsapp { get; set; }

        [Display(Name = "Telephone", Prompt = "0225859859 etc.")]
        [MaxLength(15, ErrorMessage = "Maximum 15 digits are allowed.")]
        [MinLength(10, ErrorMessage = "Minimum 10 digits are allowed.")]
        public string Telephone { get; set; }

        [Display(Name = "Telephone 2", Prompt = "0225859859 etc.")]
        [MaxLength(15, ErrorMessage = "Maximum 15 digits are allowed.")]
        [MinLength(10, ErrorMessage = "Minimum 10 digits are allowed.")]
        public string TelephoneSecond { get; set; }

        [Display(Name = "Toll Free", Prompt = "18001234567 etc.")]
        [MaxLength(30, ErrorMessage = "Maximum 30 characters allowed.")]
        [MinLength(5, ErrorMessage = "Minimum 5 characters rerquired.")]
        public string TollFree { get; set; }

        [Display(Name = "Fax", Prompt = "0225859859 etc.")]
        [MaxLength(15, ErrorMessage = "Maximum 15 digits are allowed.")]
        [MinLength(10, ErrorMessage = "Minimum 10 digits required.")]
        public string Fax { get; set; }

        [Display(Name = "Skype ID", Prompt = "john.architect etc.")]
        [MaxLength(30, ErrorMessage = "Maximum 30 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 5 characters rerquired.")]
        public string SkypeID { get; set; }
    }
}
