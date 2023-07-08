using BOL.SHARED;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.MyAccount.Profile
{
    public class UserAddressVM
    {
        public bool isVendor { get; set; }
        public DateTime DOB { get; set; }
        public string MaritialStatus { get; set; }
        public string Qualification { get; set; }
        public string Address { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public int Area { get; set; }
        public int Pincode { get; set; }
        public IList<State> States { get; set; }
        public IList<City> Cities { get; set; }
        public IList<Station> Areas { get; set; }
        public IList<Pincode> Pincodes { get; set; }

        public UserAddressVM()
        {
            States = new List<State>();
            Cities = new List<City>();
            Areas = new List<Station>();
            Pincodes = new List<Pincode>();
        }
    }
}
