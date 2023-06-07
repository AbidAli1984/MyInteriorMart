using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.AUDITTRAIL
{
    [Table("ListingClaim", Schema = "audit")]
    public class ListingClaim
    {
        [Key]
        [Display(Name = "Claim ID")]
        public int ClaimID { get; set; }

        [Display(Name = "Date Time")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Listing ID")]
        public int ListingID { get; set; }

        [Display(Name = "Claimed By")]
        public string ClaimedBy { get; set; }

        [Display(Name = "Claim Type")]
        public string ClaimType { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "OTP")]
        public int OTP { get; set; }

        [Display(Name = "OTPVerified")]
        public bool OTPVerified { get; set; }

        [Display(Name = "VerificationShortLink")]
        public string ClaimVerificationShortLink { get; set; }

        [Display(Name = "Verification Date Time")]
        public DateTime VerificationDate { get; set; }
        public string DocumentScrutinizedByStaffGuid { get; set; }
        public string DocumentApprovedDisapprovedReasonByStaff { get; set; }
    }
}
