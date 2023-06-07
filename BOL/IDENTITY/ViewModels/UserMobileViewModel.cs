using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BOL.IDENTITY.ViewModels
{
    public class UserMobileViewModel
    {
        [Required]
        public string CountryCode { get; set; }

        [Required]
        [Phone]
        public string Mobile { get; set; }

        [Required]
        public bool AcceptTerms { get; set; }
    }
}
