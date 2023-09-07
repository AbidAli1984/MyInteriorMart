using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.VIEWMODELS
{
    public class ListingActivityVM
    {
        public string OwnerGuid { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string VisitDate { get; set; }
        public int ActivityType { get; set; }
        public string ActivityText { get; set; }
    }
}
