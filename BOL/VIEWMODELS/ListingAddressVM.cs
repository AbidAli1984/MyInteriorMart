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
        public Station Assembly { get; set; }
        public Pincode Pincode { get; set; }
        public Locality Locality { get; set; }
        public string LocalAddress { get; set; }
    }
}
