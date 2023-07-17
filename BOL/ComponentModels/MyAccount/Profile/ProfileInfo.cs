using BOL.IDENTITY;
using BOL.SHARED;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.MyAccount.Profile
{
    public class ProfileInfo
    {
        public bool isVendor { get; set; }
        public UserProfile UserProfile { get; set; }
        public IList<Qualification> Qualifications { get; set; }
        public IList<Country> Countries { get; set; }
        public IList<State> States { get; set; }
        public IList<City> Cities { get; set; }
        public IList<Station> Areas { get; set; }
        public IList<Pincode> Pincodes { get; set; }
        public IList<Locality> Localities { get; set; }

        public ProfileInfo()
        {
            UserProfile = new UserProfile();
            Qualifications = new List<Qualification>();
            Countries = new List<Country>();
            States = new List<State>();
            Cities = new List<City>();
            Areas = new List<Station>();
            Pincodes = new List<Pincode>();
            Localities = new List<Locality>();
        }
    }
}
