using BOL.LISTING;
using BOL.VIEWMODELS;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public AutoCompleteMultiVM AutoCompleteMultiVM { get; set; } = new AutoCompleteMultiVM();

        public void SetViewModel(Communication communication)
        {
            Email = communication.Email;
            Mobile = communication.Mobile;
            Whatsapp = communication.Whatsapp;
            Telephone = communication.Telephone;
            TelephoneSecond = communication.TelephoneSecond;
            Website = communication.Website;
            TollFree = communication.TollFree;
            Fax = communication.Fax;
            SkypeID = communication.SkypeID;
        }

        public void SetContextModel(Communication communication)
        {
            communication.Email = Email;
            communication.Mobile = Mobile;
            communication.Whatsapp = Whatsapp;
            communication.Telephone = Telephone;
            communication.TelephoneSecond = TelephoneSecond;
            communication.Website = Website;
            communication.TollFree = TollFree;
            communication.Fax = Fax;
            communication.SkypeID = SkypeID;
            communication.Language = string.Join(", ", AutoCompleteMultiVM.itemsSelected.Select(x => x.label).ToArray());
        }

        public bool isValid()
        {
            return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Mobile) && 
                !string.IsNullOrWhiteSpace(Whatsapp) && !string.IsNullOrWhiteSpace(SkypeID) &&
                AutoCompleteMultiVM.itemsSelected.Count() > 0;
        }
    }
}
