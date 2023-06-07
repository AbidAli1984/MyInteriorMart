using System;
using System.ComponentModel.DataAnnotations;

namespace FRONTEND.BLAZOR.Models
{
    public class RoleCategoryAndRole
    {
        [Key]
        [Display(Name = "Role Category And Role ID")]
        public int RoleCategoryAndRoleID { get; set; }

        [Display(Name = "Role Category", Prompt = "Role Category")]
        [Required(ErrorMessage = "Role Category required.")]
        public Nullable<int> RoleCategoryID { get; set; }

        [Display(Name = "Role ID", Prompt = "Role ID Category")]
        [Required(ErrorMessage = "Role ID required.")]
        public string RoleID { get; set; }

        // Shafi: Navigation properties
        public virtual RoleCategory RoleCategory { get; set; }
        // End:
    }
}
