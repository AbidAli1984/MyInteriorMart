using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace BOL.LISTING
{
    [Table("Branches", Schema = "listing")]
    public class Branches
    {
        [Key]
        [Display(Name = "Branch ID")]
        public int BranchID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        [Display(Name = "Branch Name", Prompt = "Andheri West Branch etc.")]
        [MaxLength(200, ErrorMessage = "Maximum 200 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Branch name required")]
        public string BranchName { get; set; }

        [Display(Name = "Contact Person", Prompt = "John William etc.")]
        [MaxLength(200, ErrorMessage = "Maximum 200 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Contact person name required")]
        public string ContactPerson { get; set; }

        [Display(Name = "Email", Prompt = "user@xyz.com etc")]
        [MaxLength(200, ErrorMessage = "Maximum 200 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Email required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Valid email address required.")]
        public string Email { get; set; }

        [Display(Name = "Mobile", Prompt = "9587858485 etc.")]
        [Required(ErrorMessage = "Mobile required.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "10 digit mobile number.")]
        public string Mobile { get; set; }

        [Display(Name = "Telephone", Prompt = "0225859859 etc.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "10 digit phone number.")]
        public string Telephone { get; set; }

        [Display(Name = "Branch Address", Prompt = "Complete Address")]
        [MaxLength(1000, ErrorMessage = "Maximum 1000 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Branch address required")]
        [DataType(DataType.MultilineText)]
        public string BranchAddress { get; set; }
    }
}
