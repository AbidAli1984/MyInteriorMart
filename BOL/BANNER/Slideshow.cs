using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.BANNER
{
    public class Slideshow
    {
        [Key]
        [Display(Name = "Slide ID")]
        public int SlideID { get; set; }

        [Display(Name = "Alt Attribute", Prompt = "Alt Attribute")]
        [Required(ErrorMessage = "Alt Attribute required.")]
        public string AltAttribute { get; set; }

        [Display(Name = "Title Attribute", Prompt = "Title Attribute")]
        [Required(ErrorMessage = "Title Attribute required.")]
        [RegularExpression("^[a-zA-Z0-9_-]*$")]
        [MaxLength(1000, ErrorMessage = "Maximum 1000 characters allowed.")]
        [MinLength(5, ErrorMessage = "Minimum 1 characters rerquired.")]
        public string Title { get; set; }

        [Display(Name = "Target URL", Prompt = "Target URL")]
        [Required(ErrorMessage = "Target URL required.")]
        public string TargetURL { get; set; }

        [Display(Name = "Priority", Prompt = "Priority")]
        [Required(ErrorMessage = "Priority required.")]
        public int Priority { get; set; }
    }
}
