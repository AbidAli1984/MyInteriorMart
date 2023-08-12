using BOL.SHARED;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.VIEWMODELS
{
    public class ListingAddressVM
    {
        public Country Country { get; set; }
        public State State { get; set; }
        public City City { get; set; }
        public Location Assembly { get; set; }
        public Pincode Pincode { get; set; }
        public Area Locality { get; set; }
        public string LocalAddress { get; set; }
    }
}
