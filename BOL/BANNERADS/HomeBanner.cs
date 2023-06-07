using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.BANNERADS
{
    [Table("HomeBanner", Schema = "ban")]
    public class HomeBanner
    {
        [Key]
        [Display(Name = "Banner Id")]
        public int BannerId { get; set; }

        [Display(Name = "Placement")]
        [Required(ErrorMessage = "Placement required.")]
        public string Placement { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name required.")]
        public string Name { get; set; }

        [Display(Name = "Priority")]
        [Required(ErrorMessage = "Priority required.")]
        public int Priority { get; set; }

        [Display(Name = "Link Url")]
        [Required(ErrorMessage = "Link Url required.")]
        public string LinkUrl { get; set; }

        [Display(Name = "Target Window")]
        [Required(ErrorMessage = "Target Window required.")]
        public string TargetWindow { get; set; }

        [Display(Name = "Disable")]
        public bool Disable { get; set; }
    }
}
