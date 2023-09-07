using BOL.VIEWMODELS;
using System;
using System.Collections.Generic;

namespace BAL
{
    public class Constants
    {
        public static string WebRoot { get; set; }
        public const string dateFormat = "dd/MM/yyyy";
        public const string dateFormat1 = "dd MMM yyyy hh:mm tt";
        public const string dbDateFormat = "yyyy-MM-dd";
        public const string tempImagePath = @"\FileManager\tempImages\";
        public const string profileImagesPath = @"\FileManager\ProfileImages\";
        public const string ListingImagesPath = @"\FileManager\ListingImages";
        public const string ComplaintImagesPath = @"\FileManager\ComplaintImages";

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

        #region Listing Activity
        public const int Like = 1;
        public const int Bookmark = 2;
        public const int Subscribe = 3;
        #endregion

        public static IList<SearchResultViewModel> Listings;
        public static IList<SearchResultViewModel> Keywords;

        public static string[] ArrKeywords;

        public static string randomGuid { get { return Guid.NewGuid().ToString(); } }
    }
}
