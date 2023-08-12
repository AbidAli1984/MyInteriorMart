using BAL.Services.Contracts;
using BOL.SHARED;
using DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class SharedService : ISharedService
    {
        private readonly ISharedRepository sharedRepository;
        public SharedService(ISharedRepository sharedRepository)
        {
            this.sharedRepository = sharedRepository;
        }

        public async Task AddAsync(object data)
        {
            await sharedRepository.AddAsync(data);
        }

        public async Task UpdateAsync(object data)
        {
            await sharedRepository.UpdateAsync(data);
        }

        public async Task<IList<Qualification>> GetQualifications()
        {
            return await sharedRepository.GetQualifications();
        }

        public async Task<IList<Country>> GetCountries()
        {
            return await sharedRepository.GetCountries();
        }

        public async Task<IList<State>> GetStatesByCountryId(int countryId)
        {
            return await sharedRepository.GetStatesByCountryId(countryId);
        }

        public async Task<IList<City>> GetCitiesByStateId(int stateId)
        {
            return await sharedRepository.GetCitiesByStateId(stateId);
        }

        public async Task<IList<Location>> GetAreasByCityId(int cityId)
        {
            return await sharedRepository.GetAreasByCityId(cityId);
        }

        public async Task<IList<Pincode>> GetPincodesByAreaId(int areaId)
        {
            return await sharedRepository.GetPincodesByAreaId(areaId);
        }

        public async Task<IList<Area>> GetLocalitiesByPincode(int pincodeId)
        {
            return await sharedRepository.GetLocalitiesByPincode(pincodeId);
        }

        #region Address Info
        public async Task<Country> GetCountryByCountryId(int countryId)
        {
            return await sharedRepository.GetCountryByCountryId(countryId);
        }

        public async Task<State> GetStateByStateId(int stateId)
        {
            return await sharedRepository.GetStateByStateId(stateId);
        }
        public async Task<City> GetCityByCityId(int cityId)
        {
            return await sharedRepository.GetCityByCityId(cityId);
        }
        public async Task<Location> GetAreaByAreaId(int areaId)
        {
            return await sharedRepository.GetAreaByAreaId(areaId);
        }
        public async Task<Pincode> GetPincodeByPincodeId(int pincodeId)
        {
            return await sharedRepository.GetPincodeByPincodeId(pincodeId);
        }
        public async Task<Area> GetLocalityByLocalityId(int localityId)
        {
            return await sharedRepository.GetLocalityByLocalityId(localityId);
        }
        public async Task<Location> GetAreaByAreaName(string area)
        {
            return await sharedRepository.GetAreaByAreaName(area);
        }
        public async Task<Pincode> GetPincodeByPinNumber(int pinNumber)
        {
            return await sharedRepository.GetPincodeByPinNumber(pinNumber);
        }
        public async Task<Area> GetLocalityByLocalityName(string localityName)
        {
            return await sharedRepository.GetLocalityByLocalityName(localityName);
        }
        #endregion

        #region Company Info
        public async Task<IList<NatureOfBusiness>> GetNatureOfBusinesses()
        {
            return await sharedRepository.GetNatureOfBusinesses();
        }
        public async Task<IList<Designation>> GetDesignations()
        {
            return await sharedRepository.GetDesignations();
        }
        #endregion
    }
}
