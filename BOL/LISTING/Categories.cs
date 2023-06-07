using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.LISTING
{
    [Table("Categories", Schema = "listing")]
    public class Categories
    {
        [Key]
        [Display(Name = "Category ID")]
        public int CategoryID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        [Display(Name = "First Category", Prompt = "Select First Category")]
        [Required(ErrorMessage = "First Category Required")]
        public int FirstCategoryID { get; set; }

        [Display(Name = "Second Category", Prompt = "Select Second Category")]
        [Required(ErrorMessage = "Second Category Required")]
        public int SecondCategoryID { get; set; }

        [Display(Name = "Third Categories", Prompt = "Select Third Categories")]
        public string ThirdCategories { get; set; }

        [Display(Name = "Fourth Categories", Prompt = "Select Fourth Categories")]
        public string FourthCategories { get; set; }

        [Display(Name = "Fifth Categories", Prompt = "Select Fifth Categories")]
        public string FifthCategories { get; set; }

        [Display(Name = "Sixth Categories", Prompt = "Select Sixth Categories")]
        public string SixthCategories { get; set; }
    }
}
