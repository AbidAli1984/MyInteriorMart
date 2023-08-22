using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.LISTING.UploadImage
{
    [Table("OwnerImage")]
    public class OwnerImage
    {
        [Key]
        [Display(Name = "Image ID")]
        public int Id { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "Owner Image")]
        [Required(ErrorMessage = "Owner Image required.")]
        public string ImagePath { get; set; }

        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Designation required.")]
        public string Designation { get; set; }

        [Display(Name = "Religion")]
        [Required(ErrorMessage = "Religion required.")]
        public int ReligionId { get; set; }

        [Display(Name = "Cast")]
        [Required(ErrorMessage = "Cast required.")]
        public int CastId { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Select Country")]
        public int CountryID { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Select State")]
        public int StateID { get; set; }

        [Display(Name = "Owner Name")]
        [Required(ErrorMessage = "Owner Name required.")]
        public string OwnerName { get; set; }

        [Display(Name = "Created Date")]
        [Required(ErrorMessage = "Created Date required.")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Updated Date")]
        [Required(ErrorMessage = "Updated Date required.")]
        public DateTime UpdateDate { get; set; }
    }
}
