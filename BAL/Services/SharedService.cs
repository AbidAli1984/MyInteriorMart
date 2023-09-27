using BAL.Services.Contracts;
using BOL.SHARED;
using BOL.VIEWMODELS;
using DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<Location>> GetLocalitiesByCityId(int cityId)
        {
            return await sharedRepository.GetLocalitiesByCityId(cityId);
        }

        public async Task<IList<Pincode>> GetPincodesByLocalityId(int areaId)
        {
            return await sharedRepository.GetPincodesByLocalityId(areaId);
        }

        public async Task<IList<Area>> GetAreasByPincodeId(int pincodeId)
        {
            return await sharedRepository.GetAreasByPincodeId(pincodeId);
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
        public async Task<Location> GetLocalityByLocalityId(int localityId)
        {
            return await sharedRepository.GetLocalityByLocalityId(localityId);
        }
        public async Task<Pincode> GetPincodeByPincodeId(int pincodeId)
        {
            return await sharedRepository.GetPincodeByPincodeId(pincodeId);
        }
        public async Task<Area> GetAreaByAreaId(int areaId)
        {
            return await sharedRepository.GetAreaByAreaId(areaId);
        }
        public async Task<Location> GetLocalityByLocalityName(string locality)
        {
            return await sharedRepository.GetLocalityByLocalityName(locality);
        }
        public async Task<Pincode> GetPincodeByPinNumber(int pinNumber)
        {
            return await sharedRepository.GetPincodeByPinNumber(pinNumber);
        }
        public async Task<Area> GetAreaByAreaName(string area)
        {
            return await sharedRepository.GetAreaByAreaName(area);
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

        public async Task<IList<Religion>> GetReligions()
        {
            return await sharedRepository.GetReligions();
        }

        public async Task<IList<Caste>> GetCastesByReligionId(int religionId)
        {
            return await sharedRepository.GetCastesByReligionId(religionId);
        }

        public async Task<IList<SearchResultViewModel>> GetLanguages()
        {
            var languages = await sharedRepository.GetLanguages();

            return languages
                .Select(x => new SearchResultViewModel { label = x.Name, value = x.Name })
                .ToList();
        }
    }
}
