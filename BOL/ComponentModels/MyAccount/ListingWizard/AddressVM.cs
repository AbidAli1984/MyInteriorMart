using BOL.SHARED;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.MyAccount.ListingWizard
{
    public class AddressVM
    {
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int StationId { get; set; }
        public int PincodeId { get; set; }
        public int LocalityId { get; set; }
        public string Address { get; set; }

        public IList<Country> Countries { get; set; }
        public IList<State> States { get; set; }
        public IList<City> Cities { get; set; }
        public IList<Station> Areas { get; set; }
        public IList<Pincode> Pincodes { get; set; }
        public IList<Locality> Localities { get; set; }

        public AddressVM()
        {
            Countries = new List<Country>();
            States = new List<State>();
            Cities = new List<City>();
            Areas = new List<Station>();
            Pincodes = new List<Pincode>();
            Localities = new List<Locality>();
        }

        public bool isValid()
        {
            return CountryId > 0 && StateId > 0 && CityId > 0 && StationId > 0 && 
                PincodeId > 0 && LocalityId > 0 && !string.IsNullOrEmpty(Address);
        }
    }
}
