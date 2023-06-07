using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.LISTING
{
    [Table("PaymentMode", Schema = "listing")]
    public class PaymentMode
    {
        [Key]
        [Display(Name = "Payment ID")]
        public int PaymentID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        [Display(Name = "Cash")]
        public bool Cash { get; set; }

        [Display(Name = "Net Banking")]
        public bool NetBanking { get; set; }

        [Display(Name = "Cheque")]
        public bool Cheque { get; set; }

        [Display(Name = "RTGS & NEFT")]
        public bool RtgsNeft { get; set; }

        [Display(Name = "Debit Card")]
        public bool DebitCard { get; set; }

        [Display(Name = "Credit Card")]
        public bool CreditCard { get; set; }

        [Display(Name = "PayTM")]
        public bool PayTM { get; set; }

        [Display(Name = "Phone Pay")]
        public bool PhonePay { get; set; }

        [Display(Name = "Paypal")]
        public bool Paypal { get; set; }
    }
}
