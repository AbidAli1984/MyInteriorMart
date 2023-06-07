using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BOL.PLAN;

namespace BOL.BILLING
{
    [Table("Orders", Schema = "bill")]
    public class Orders
    {
        [Key]
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        // Shafi: User GUID
        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }
        // End:

        // Shafi: Shopping Cart ID
        [Display(Name = "Subscription ID")]
        [Required(ErrorMessage = "Subscription is required.")]
        public Nullable<int> SubscriptionID { get; set; }
        // End:

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        // Shafi: Date and time
        [Display(Name = "Date Of Order")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Time of Order")]
        [DataType(DataType.Time)]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "Date Of Modify")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ModifyDate { get; set; }
        // End:

        // Shafi: Razorpay payment gateway integration
        // Documentation: https://razorpay.com/docs/payment-gateway/payment-flow/

        [Display(Name = "Razorpay Order ID")]
        public string RazorpayOrderID { get; set; }

        [Display(Name = "Razorpay Payment ID")]
        public string RazorpayPaymentID { get; set; }

        [Display(Name = "Razorpay Signature")]
        public string RazorpaySignature { get; set; }
        // End: Razorpay

        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }

        [Display(Name = "Coupon Code")]
        public string CouponCode { get; set; }

        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [Display(Name = "OrderAmount")]
        public decimal OrderAmount { get; set; }

        [Display(Name = "Accepted Terms & Conditions")]
        public bool AcceptedTermsConditions { get; set; }

        // Begin: Navigation properties
        public virtual Subscription Subscription { get; set; }
        // End:
    }
}
