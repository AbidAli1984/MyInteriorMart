using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Utils;

namespace BOL.ComponentModels.MyAccount.ListingWizard
{
    public class CommunicationVM
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Whatsapp { get; set; }
        public string Telephone { get; set; }
        public string TelephoneSecond { get; set; }
        public string Website { get; set; }
        public string TollFree { get; set; }
        public string Fax { get; set; }
        public string SkypeID { get; set; }

        public string EmailErrMessage
        {
            get { return FieldValidator.emailErrMessage(Email); }
        }

        public string MobileErrMessage
        {
            get { return FieldValidator.mobileErrMessage(Mobile); }
        }

        public string WhatsappErrMessage
        {
            get { return FieldValidator.mobileErrMessage(Whatsapp); }
        }

        public string WebsiteErrorMessage
        {
            get { return FieldValidator.websiteErrMessage(Website); }
        }

        public bool isValid()
        {
            return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Mobile) && 
                !string.IsNullOrWhiteSpace(Whatsapp) && !string.IsNullOrWhiteSpace(SkypeID);
        }
    }
}
