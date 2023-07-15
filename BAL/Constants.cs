using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
