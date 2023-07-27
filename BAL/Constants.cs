using BOL.VIEWMODELS;
using System.Collections.Generic;

namespace BAL
{
    public class Constants
    {
        public static string WebRoot;

        #region Category Id
        public static int Cat_Repairs = 40;
        public static int Cat_Services = 41;
        public static int Cat_Contractors = 43;
        public static int Cat_Dealers = 45;
        public static int Cat_Manufacturers = 45;
        #endregion

        #region HTTP Status Codes
        public static int Success = 200;
        public static int Created = 201;
        public static int BadRequest = 400;
        public static int Unauthorized = 401;
        #endregion

        #region Category Level
        public static string LevelFirstCategory = "fc";
        public static string LevelSecondCategory = "sc";
        public static string LevelThirdCategory = "tc";
        public static string LevelFourthCategory = "ivc";
        public static string LevelFifthCategory = "vc";
        public static string LevelSixthCategory = "vic";
        #endregion

        #region Weekdays Name
        public static string Sunday = "Sunday";
        public static string Monday = "Monday";
        public static string Tuesday = "Tuesday";
        public static string Wednesday = "Wednesday";
        public static string Thursday = "Thursday";
        public static string Friday = "Friday";
        public static string Saturday = "Saturday";
        #endregion

        public static IList<SearchResultViewModel> Listings;
    }
}
