using BOL.LISTING;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.MyAccount.ListingWizard
{
    public class PaymentModeVM
    {
        public bool Cash { get; set; } = false;
        public bool NetBanking { get; set; }
        public bool Cheque { get; set; } = false;
        public bool RtgsNeft { get; set; }
        public bool DebitCard { get; set; }
        public bool CreditCard { get; set; }
        public bool PayTM { get; set; }
        public bool PhonePay { get; set; }
        public bool Paypal { get; set; }

        public void SetViewModel(PaymentMode paymentMode)
        {
            Cash = paymentMode.Cash;
            NetBanking = paymentMode.NetBanking;
            Cheque = paymentMode.Cheque;
            RtgsNeft = paymentMode.RtgsNeft;
            DebitCard = paymentMode.DebitCard;
            CreditCard = paymentMode.CreditCard;
            PayTM = paymentMode.PayTM;
            PhonePay = paymentMode.PhonePay;
            Paypal = paymentMode.Paypal;
        }

        public void SetPaymentMode(PaymentMode paymentMode)
        {
            paymentMode.Cash = Cash;
            paymentMode.NetBanking = NetBanking;
            paymentMode.Cheque = Cheque;
            paymentMode.RtgsNeft = RtgsNeft;
            paymentMode.DebitCard = DebitCard;
            paymentMode.CreditCard = CreditCard;
            paymentMode.PayTM = PayTM;
            paymentMode.PhonePay = PhonePay;
            paymentMode.Paypal = Paypal;
        }

        public void selectOrUnselectAll(bool isSelect = true)
        {
            Cash = isSelect;
            NetBanking = isSelect;
            Cheque = isSelect;
            RtgsNeft = isSelect;
            DebitCard = isSelect;
            CreditCard = isSelect;
            PayTM = isSelect;
            PhonePay = isSelect;
            Paypal = isSelect;
        }

        public bool isValid()
        {
            return Cash || NetBanking || Cheque || RtgsNeft || DebitCard || CreditCard || PayTM || PhonePay || Paypal;
        }
    }
}
