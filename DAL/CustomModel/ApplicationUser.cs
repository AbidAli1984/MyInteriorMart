using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.CustomModel
{
    public class ApplicationUser : IdentityUser
    {
        public string Otp { get; set; }
    }
}
