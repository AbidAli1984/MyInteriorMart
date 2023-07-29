using BOL.VIEWMODELS;
using System.Collections.Generic;

namespace BAL
{
    public class Constants
    {
        public static string WebRoot { get; set; }

        #region Category Id
        public const int Cat_Repairs = 40;
        public const int Cat_Services = 41;
        public const int Cat_Contractors = 43;
        public const int Cat_Dealers = 45;
        public const int Cat_Manufacturers = 45;
        #endregion

        #region HTTP Status Codes
        public const int Success = 200;
        public const int Created = 201;
        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        #endregion

        #region Category Level
        public const string LevelFirstCategory = "fc";
        public const string LevelSecondCategory = "sc";
        public const string LevelThirdCategory = "tc";
        public const string LevelFourthCategory = "ivc";
        public const string LevelFifthCategory = "vc";
        public const string LevelSixthCategory = "vic";
        #endregion

        #region Weekdays Name
        public const string Sunday = "Sunday";
        public const string Monday = "Monday";
        public const string Tuesday = "Tuesday";
        public const string Wednesday = "Wednesday";
        public const string Thursday = "Thursday";
        public const string Friday = "Friday";
        public const string Saturday = "Saturday";
        #endregion

        #region ListingWizard Steps
        public const int CompanyComplete = 1;
        public const int CommunicationComplete = 2;
        public const int AddressComplete = 3;
        public const int CategoryComplete = 4;
        public const int SpecialisationComplete = 5;
        public const int WorkingHourComplete = 6;
        public const int PaymentModeComplete = 7;
        #endregion

        public static IList<SearchResultViewModel> Listings;
    }
}
