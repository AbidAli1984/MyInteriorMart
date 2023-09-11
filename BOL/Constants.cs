using System;
using System.Collections.Generic;
using System.Text;

namespace BOL
{
    public class Constants
    {
        public const string dateFormat1 = "dd MMM yyyy hh:mm tt";

        #region Side Navigations
        public static int EditProfile = 1;
        public static int PersonalInfo = 2;
        public static int ManageListing = 3;
        public static int MyListings = 4;
        public static int Offers = 5;
        public static int Review = 6;
        public static int Like = 7;
        public static int Bookmark = 8;
        public static int Subscribe = 9;
        public static int Settings = 10;
        public static int Suggestion = 11;
        public static int Complaint = 12;
        public static int Enquiry = 13;
        public static int Notification = 14;
        public static int Chat = 15;
        public static int History = 16;
        public static int ChangePassword = 17;
        public static int MyActivity = 18;
        public static int Dashboard = 19;
        #endregion

        #region Business
        public static string Open = "Open";
        public static string Closed = "Closed Now";
        #endregion

        #region ListingWizard Steps
        public const int CompanyComplete = 1;
        public const int CommunicationComplete = 2;
        public const int AddressComplete = 3;
        public const int CategoryComplete = 4;
        public const int SpecialisationComplete = 5;
        public const int WorkingHourComplete = 6;
        public const int PaymentModeComplete = 7;
        public const int UploadImageComplete = 8;
        public const int SocialLinkComplete = 9;
        public const int SEOKeywordComplete = 10;
        #endregion

        private const string ShareCompanyTagLine = "Check this out on MyInteriorMart!";

        #region Share Via Social Links
        public const string FacebookLink = "https://www.facebook.com/sharer/sharer.php?quote=" + ShareCompanyTagLine;
        public const string WhatsappLink = "https://web.whatsapp.com/send?text=" + ShareCompanyTagLine;
        #endregion
    }
}
