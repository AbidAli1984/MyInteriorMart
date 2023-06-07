using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.IDENTITY
{
    [Table("RoleCategory", Schema = "dbo")]
    public class RoleCategory
    {
        [Key]
        [Display(Name = "Role Category ID")]
        public int RoleCategoryID { get; set; }

        [Display(Name = "Priority")]
        //[Required(ErrorMessage = "Priority required.")]
        public int Priority { get; set; }

        [Display(Name = "Category Name", Prompt = "Category Name")]
        [Required(ErrorMessage = "Category Name required.")]
        public string CategoryName { get; set; }

        // Shafi: Show Role Category in RoleCategoryAndRole table
        public IList<RoleCategoryAndRole> RoleCategoryAndRole { get; set; }
        // End:
    }
}
