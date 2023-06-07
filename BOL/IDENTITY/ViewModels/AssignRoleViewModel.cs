using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BOL.IDENTITY.ViewModels
{
    public class AssignRoleViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
