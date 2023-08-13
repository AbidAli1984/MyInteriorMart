using BOL.SHARED;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.SHARED;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BAL.Audit;

namespace BAL.Addresses
{
    public class Addresses : IAddresses
    {
        private readonly SharedDbContext sharedContext;
        public Addresses(SharedDbContext sharedContext)
        {
            this.sharedContext = sharedContext;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            var result = await sharedContext.City.OrderBy(i => i.Name).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            var result = await sharedContext.Country.OrderBy(i => i.Name).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Area>> GetLocalitiesAsync()
        {
            var result = await sharedContext.Area.OrderBy(i => i.Name).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Pincode>> GetPincodesAsync()
        {
            var result = await sharedContext.Pincode.OrderBy(i => i.PincodeNumber).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<State>> GetStatesAsync()
        {
            var result = await sharedContext.State.OrderBy(i => i.Name).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Location>> GetStationsAsync()
        {
            var result = await sharedContext.Location.OrderBy(i => i.Name).ToListAsync();
            return result;
        }

        // Shafi: Get Names By ID
        public async Task<Country> CountryDetailsAsync(int CountryId)
        {
            var country = await sharedContext.Country.Where(c => c.CountryID == CountryId).FirstOrDefaultAsync();
            return country;
        }

        public async Task<State> StateDetailsAsync(int StateId)
        {
            var state = await sharedContext.State.Where(c => c.StateID == StateId).FirstOrDefaultAsync();
            return state;
        }

        public async Task<City> CityDetailsAsync(int CityId)
        {
            var city = await sharedContext.City.Where(c => c.CityID == CityId).FirstOrDefaultAsync();
            return city;
        }

        public async Task<Location> StationDetailsAsync(int StationId)
        {
            var station = await sharedContext.Location.Where(c => c.Id == StationId).FirstOrDefaultAsync();
            return station;
        }

        public async Task<Pincode> PincodeDetailsAsync(int PincodeId)
        {
            var pincode = await sharedContext.Pincode.Where(c => c.PincodeID == PincodeId).FirstOrDefaultAsync();
            return pincode;
        }

        public async Task<Area> LocalityDetailsAsync(int LocalityId)
        {
            var locality = await sharedContext.Area.Where(c => c.Id == LocalityId).FirstOrDefaultAsync();
            return locality;
        }
        // End:

        public string CountryName(int id)
        {
            var countryName = sharedContext.Country.Where(i => i.CountryID == id).Select(i => i.Name).FirstOrDefault();
            return countryName;
        }

        public string StateName(int id)
        {
            var stateName = sharedContext.State.Where(i => i.StateID == id).Select(i => i.Name).FirstOrDefault();
            return stateName;
        }

        public string CityName(int id)
        {
            var cityName = sharedContext.City.Where(i => i.CityID == id).Select(i => i.Name).FirstOrDefault();
            return cityName;
        }

        public string AssemblyName(int id)
        {
            var assemblyName = sharedContext.Location.Where(i => i.Id == id).Select(i => i.Name).FirstOrDefault();
            return assemblyName;
        }

        public int pincode(int id)
        {
            var pincodeNumber = sharedContext.Pincode.Where(i => i.PincodeID == id).Select(i => i.PincodeNumber).FirstOrDefault();
            return pincodeNumber;
        }

        public int CountStateInCountry(int countryId)
        {
            return sharedContext.State.Where(i => i.CountryID == countryId).Count();
        }

        public int CountCityInState(int stateId)
        {
            return sharedContext.City.Where(i => i.StateID == stateId).Count();
        }

        public int CountStationInCity(int cityId)
        {
            return sharedContext.Location.Where(i => i.CityID == cityId).Count();
        }

        public int CountPincodeInStation(int stationId)
        {
            return sharedContext.Pincode.Where(i => i.LocationId == stationId).Count();
        }

        public int CountAreaInPincode(int pincodeId)
        {
            return sharedContext.Area.Where(i => i.PincodeID == pincodeId).Count();
        }
    }
}
