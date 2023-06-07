using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.AUDITTRAIL
{
    [Table("UserRegistrationOTPVerification", Schema = "audit")]
    public class UserRegistrationOTPVerification
    {
        [Key]
        [Display(Name = "OTP ID")]
        public int OtpID { get; set; }

        [Display(Name = "Date Time")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "OTP")]
        public int OTP { get; set; }

        [Display(Name = "OTPVerified")]
        public bool OTPVerified { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}
