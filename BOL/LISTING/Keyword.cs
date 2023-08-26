using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace BOL.LISTING
{
    [Table("Keyword")]
    public class Keyword
    {
        [Key]
        [Display(Name = "Keyword ID")]
        public int KeywordID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "Seo Keyword")]
        [Required(ErrorMessage = "Seo Keyword Required")]
        public string SeoKeyword { get; set; }
    }
}
