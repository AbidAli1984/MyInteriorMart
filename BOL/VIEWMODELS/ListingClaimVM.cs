using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.VIEWMODELS
{
    public class ListingClaimVM
    {
        public string MobileOrEmail { get; set; }
        public string OTP { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
