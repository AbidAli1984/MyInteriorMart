using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.BANNER
{
    [Table("BannerPage", Schema = "banner")]
    public class BannerPage
    {
        [Key]
        [Display(Name = "Banner Page ID")]
        public int BannerPageID { get; set; }

        [Display(Name = "Page Name", Prompt = "Plan Name")]
        [Required(ErrorMessage = "Page Name required.")]
        public string PageName { get; set; }

        // Shafi: Show in
        public IList<Campaign> Campaign { get; set; }
        public IList<BannerSpace> BannerSpace { get; set; }
        // End:
    }
}
