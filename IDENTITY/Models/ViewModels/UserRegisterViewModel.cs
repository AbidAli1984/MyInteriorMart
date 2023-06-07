using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IDENTITY.Models.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [Phone]
        public string Mobile { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
