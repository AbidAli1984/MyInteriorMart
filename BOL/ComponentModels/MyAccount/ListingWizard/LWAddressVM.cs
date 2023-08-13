using BOL.LISTING;
using BOL.SHARED;
using System.Collections.Generic;

namespace BOL.ComponentModels.MyAccount.ListingWizard
{
    public class LWAddressVM
    {
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int LocalityId { get; set; }
        public int PincodeId { get; set; }
        public int AreaId { get; set; }
        public string Address { get; set; }
        public bool IsFirstLoad { get; set; } = true;

        public bool IsCountryChange { get; set; }
        public bool IsStateChange { get; set; }
        public bool IsCityChange { get; set; }
        public bool IsLocalityChange { get; set; }
        public bool IsPincodeChange { get; set; }
        public bool IsAreaChange { get; set; }

        public IList<State> States { get; set; }
        public IList<City> Cities { get; set; }
        public IList<Location> Localities { get; set; }
        public IList<Pincode> Pincodes { get; set; }
        public IList<Area> Areas { get; set; }

        public LWAddressVM()
        {
            States = new List<State>();
            Cities = new List<City>();
            Localities = new List<Location>();
            Pincodes = new List<Pincode>();
            Areas = new List<Area>();
        }

        public void SetViewModel(Address address)
        {
            CountryId = address.CountryID;
            StateId = address.StateID;
            CityId = address.City;
            LocalityId = address.AssemblyID;
            PincodeId = address.PincodeID;
            AreaId = address.LocalityID;
            Address = address.LocalAddress;
        }

        public void SetContextModel(Address address)
        {
            address.CountryID = CountryId;
            address.StateID = StateId;
            address.City = CityId;
            address.AssemblyID = LocalityId;
            address.PincodeID = PincodeId;
            address.LocalityID = AreaId;
            address.LocalAddress = Address;
        }

        public bool isValid()
        {
            return CountryId > 0 && StateId > 0 && CityId > 0 && LocalityId > 0 && 
                PincodeId > 0 && AreaId > 0 && !string.IsNullOrEmpty(Address);
        }
    }
}
