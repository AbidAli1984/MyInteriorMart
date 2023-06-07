using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDENTITY.Models.ViewModels
{
    public class EditRoleViewModel
    {
        public string RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is Required")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
