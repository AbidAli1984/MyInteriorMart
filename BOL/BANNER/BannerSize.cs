using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.BANNER
{
    [Table("BannerSize", Schema = "banner")]
    public class BannerSize
    {
        [Key]
        [Display(Name = "Size ID")]
        public int SizeID { get; set; }

        [Display(Name = "Name", Prompt = "Name")]
        [Required(ErrorMessage = "Name required.")]
        public string Name { get; set; }

        [Display(Name = "Width", Prompt = "Width in Pixels")]
        [Required(ErrorMessage = "Width required.")]
        public string Width { get; set; }

        [Display(Name = "Height", Prompt = "Height in Pixels")]
        [Required(ErrorMessage = "Height required.")]
        public string Height { get; set; }

        // Shafi: Show Banner Size in
        // End:
    }
}
