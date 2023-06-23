using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Otp { get; set; }
    }
}
