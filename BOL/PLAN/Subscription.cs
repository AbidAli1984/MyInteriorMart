using BOL.BILLING;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.PLAN
{
    [Table("Subscription", Schema = "plan")]
    public class Subscription
    {
        [Key]
        [Display(Name = "Subscription ID")]
        public int SubscriptionID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        // Shafi: Date and time
        [Display(Name = "Start Date")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Date Of Modify")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ModifyDate { get; set; }

        // Shafi: Razorpay payment gateway integration
        // Documentation: https://razorpay.com/docs/payment-gateway/payment-flow/

        [Display(Name = "Razorpay Order ID")]
        public string RazorpayOrderID { get; set; }

        [Display(Name = "Razorpay Payment ID")]
        public string RazorpayPaymentID { get; set; }

        [Display(Name = "Razorpay Signature")]
        public string RazorpaySignature { get; set; }
        // End: Razorpay

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Select Product")]
        public Nullable<int> ProductID { get; set; }

        [Display(Name = "Period")]
        [Required(ErrorMessage = "Select Period")]
        public Nullable<int> PeriodID { get; set; }

        [Display(Name = "Payment Method", Prompt = "Cash, Cheque etc.")]
        [Required(ErrorMessage = "Payment Method Required.")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Payment Status", Prompt = "Done, Pending etc.")]
        [Required(ErrorMessage = "Payment Method Required.")]
        public string PaymentStatus { get; set; }

        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }

        [Display(Name = "Coupon Code")]
        public string CouponCode { get; set; }

        [Display(Name = "OrderAmount")]
        public decimal OrderAmount { get; set; }

        [Display(Name = "Accepted Terms & Conditions")]
        public bool AcceptedTermsConditions { get; set; }

        // Shafi: Relationships
        public virtual Product Product { get; set; }
        public virtual Period Period { get; set; }
        // End:

        // Shafi: Show Subscription in
        public IList<Orders> Orders { get; set; }
        // End:
    }
}
