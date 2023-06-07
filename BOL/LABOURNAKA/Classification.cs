using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LABOURNAKA
{
    [Table("Classification", Schema = "lab")]
    public class Classification
    {
        [Key]
        [Display(Name = "Classification Id")]
        public int ClassificationId { get; set; }

        [Display(Name = "User Guid")]
        [Required(ErrorMessage = "User Guid Required")]
        public string UserGuid { get; set; }

        [Display(Name = "Mistry OR Labour")]
        public string MistryORLabour { get; set; }

        [Display(Name = "Parent Category Id")]
        [Required(ErrorMessage = "Parent Category Id Required")]
        public Nullable<int> ParentCategoryId { get; set; }

        [Display(Name = "Child Category Id")]
        [Required(ErrorMessage = "Child Category Id Required")]
        public Nullable<int> ChildCategoryId { get; set; }

        // Shafi: Navigation properties
        public virtual LaborCategory LaborCategory { get; set; }
    }
}
