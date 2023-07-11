using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BOL.ComponentModels.MyAccount.Auth
{
    public class UserRegisterVM
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string emailErrMessage { get; set; }
        public string mobileErrMessage { get; set; }
        public string passwordErrMessage { get; set; }
        public string confirmPasswordErrMessage { get; set; }
        public bool isVendor { get; set; }
        public string OTP { get; set; }
        public string ConfOTP { get; set; }
    }
}
