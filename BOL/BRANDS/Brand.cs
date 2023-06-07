using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.BRANDS
{
    [Table("Brand", Schema = "brands")]
    public class Brand
    {
        [Key]
        [Display(Name = "Brand ID")]
        public int BrandID { get; set; }

        [Required(ErrorMessage = "Brand Name Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "URL Required")]
        public string URL { get; set; }

        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }

        // Show brands in 
        public IList<BrandCategory> BrandCategory { get; set; }
        // End:
    }
}
