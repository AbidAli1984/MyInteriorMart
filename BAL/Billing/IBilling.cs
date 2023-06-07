using BOL.BILLING;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Billing
{
    public interface IBilling
    {
        Task CreateOrderAsync(int ListingID, string OwnerGuid, int SubscriptionID, string IPAddress, DateTime CreatedOn, DateTime CreatedTime, DateTime ModifyDate, string RazorpayOrderID, string RazorpayPaymentID, string RazorpaySignature, string OrderStatus, string CouponCode, string PaymentMethod, decimal OrderAmount, bool AcceptedTermsConditions);
    }
}
