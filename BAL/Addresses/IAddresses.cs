using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BOL.SHARED;

namespace BAL.Addresses
{
    public interface IAddresses
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
        Task<IEnumerable<State>> GetStatesAsync();
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<IEnumerable<Station>> GetStationsAsync();
        Task<IEnumerable<Pincode>> GetPincodesAsync();
        Task<IEnumerable<Locality>> GetLocalitiesAsync();

        Task<Country> CountryDetailsAsync(int CountryId);
        Task<State> StateDetailsAsync(int StateId);
        Task<City> CityDetailsAsync(int CityId);
        Task<Station> StationDetailsAsync(int StationId);
        Task<Pincode> PincodeDetailsAsync(int PincodeId);
        Task<Locality> LocalityDetailsAsync(int LocalityId);

        string CountryName(int id);
        string StateName(int id);
        string CityName(int id);
        string AssemblyName(int id);
        int pincode(int id);

        int CountStateInCountry(int countryId);
        int CountCityInState(int stateId);
        int CountStationInCity(int cityId);
        int CountPincodeInStation(int stationId);
        int CountAreaInPincode(int pincodeId);
    }
}
