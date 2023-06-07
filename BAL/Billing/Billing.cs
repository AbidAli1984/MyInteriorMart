using BOL.BILLING;
using DAL.BILLING;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Billing
{
    public class Billing : IBilling
    {

        private readonly BillingDbContext billingContext;
        public Billing(BillingDbContext billingContext)
        {
            this.billingContext = billingContext;
        }

        public async Task CreateOrderAsync(int ListingID, string OwnerGuid, int SubscriptionID, string IPAddress, DateTime CreatedOn, DateTime CreatedTime, DateTime ModifyDate, string RazorpayOrderID, string RazorpayPaymentID, string RazorpaySignature, string OrderStatus, string CouponCode, string PaymentMethod, decimal OrderAmount, bool AcceptedTermsConditions)
        {
            Orders order = new Orders();
            order.ListingID = ListingID;
            order.OwnerGuid = OwnerGuid;
            order.SubscriptionID = SubscriptionID;
            order.IPAddress = IPAddress;
            order.CreatedOn = CreatedOn;
            order.CreatedTime = CreatedTime;
            order.ModifyDate = ModifyDate;
            order.RazorpayOrderID = RazorpayOrderID;
            order.RazorpayPaymentID = RazorpayPaymentID;
            order.RazorpaySignature = RazorpaySignature;
            order.OrderStatus = OrderStatus;
            order.CouponCode = CouponCode;
            order.PaymentMethod = PaymentMethod;
            order.OrderAmount = OrderAmount;
            order.AcceptedTermsConditions = AcceptedTermsConditions;

            await billingContext.AddAsync(order);
            await billingContext.SaveChangesAsync();
        }
    }
}
