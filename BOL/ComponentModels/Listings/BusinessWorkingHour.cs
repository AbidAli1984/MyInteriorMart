using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.Listings
{
    public class BusinessWorkingHour
    {
        public bool IsBusinessOpen { get; set; }
        public string IsBusinessOpenText
        {
            get
            {
                return IsBusinessOpen ? Constants.Open : Constants.Closed;
            }
        }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string OpenDay { get; set; }
    }
}
