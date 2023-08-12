using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LISTING.UploadImage
{
    [Table("ClientDetail")]
    public class ClientDetail
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

        [Display(Name = "Client Image")]
        [Required(ErrorMessage = "Client Image required.")]
        public string ImagePath { get; set; }

        [Display(Name = "Image Title")]
        [Required(ErrorMessage = "Image Title required.")]
        public string ImageTitle { get; set; }

        [Display(Name = "Created Date")]
        [Required(ErrorMessage = "Created Date required.")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Updated Date")]
        [Required(ErrorMessage = "Updated Date required.")]
        public DateTime UpdateDate { get; set; }
    }
}
