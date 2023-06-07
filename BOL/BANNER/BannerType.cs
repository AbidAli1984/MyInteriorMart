using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.BANNER
{
    [Table("BannerType", Schema = "banner")]
    public class BannerType
    {
        [Key]
        [Display(Name = "Banner Type ID")]
        public int BannerTypeID { get; set; }

        [Display(Name = "Banner Type", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Banner type required.")]
        public string Type { get; set; }

        // Shafi: Show Banner Type in
        public IList<Campaign> Campaign { get; set; }
        // End:
    }
}
