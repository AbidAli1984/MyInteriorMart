using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.BANNER
{
    [Table("BannerSpace", Schema = "banner")]
    public class BannerSpace
    {
        [Key]
        [Display(Name = "Banner Space ID")]
        public int BannerSpaceID { get; set; }

        [Display(Name = "Page", Prompt = "Page")]
        [Required(ErrorMessage = "Page required.")]
        public Nullable<int> BannerPageID { get; set; }

        [Display(Name = "Space Name", Prompt = "Space Name")]
        [Required(ErrorMessage = "Space Name required.")]
        public string SpaceName { get; set; }

        [Display(Name = "Width", Prompt = "Width in Pixels")]
        [Required(ErrorMessage = "Width required.")]
        public string Width { get; set; }

        [Display(Name = "Height", Prompt = "Height in Pixels")]
        [Required(ErrorMessage = "Height required.")]
        public string Height { get; set; }

        // Shafi: Navigation properties
        public virtual BannerPage BannerPage { get; set; }
        // End:

        // Shafi: Show in
        public IList<Campaign> Campaign { get; set; }
        // End:
    }
}
