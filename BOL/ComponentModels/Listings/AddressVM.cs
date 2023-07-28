using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.Listings
{
    public class AddressVM
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Assembly { get; set; }
        public int Pincode { get; set; }
        public string Locality { get; set; }
        public string LocalAddress { get; set; }
    }
}
