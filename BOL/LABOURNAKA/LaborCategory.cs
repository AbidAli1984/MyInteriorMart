using BOL.SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LABOURNAKA
{
    [Table("LaborCategory", Schema = "lab")]
    public class LaborCategory
    {
        [Key]
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [Display(Name = "Url")]
        [Required(ErrorMessage = "Url Required")]
        public string Url { get; set; }

        [Display(Name = "Meta Title")]
        [Required(ErrorMessage = "Meta Title Required")]
        public string MetaTitle { get; set; }

        [Display(Name = "Meta Description")]
        [Required(ErrorMessage = "Meta Description Required")]
        public string MetaDescription { get; set; }

        // Child Category
        public bool IsChild { get; set; }
        public int ParentCategoryId { get; set; }

        // Show In
        public IList<Classification> Classification { get; set; }
    }
}
