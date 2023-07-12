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
        public string NewPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string EmailErrMessage { get; set; }
        public string MobileErrMessage { get; set; }
        public string NewPasswordErrMessage { get; set; }
        public string PasswordErrMessage { get; set; }
        public string ConfirmPasswordErrMessage { get; set; }
        public bool IsVendor { get; set; }
        public string UserOtp { get; set; }
    }
}
