using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.VIEWMODELS.Hub
{
    public class ConnectedUsersViewModel
    {
        public string userType { get; set; }
        public string userName { get; set; }
        public string profileImage { get; set; }
        public string connectionId { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string postal { get; set; }
        public string url { get; set; }
        public string referrer { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
