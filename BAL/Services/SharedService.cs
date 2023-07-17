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

        public async Task<IList<Station>> GetAreasByCityId(int cityId)
        {
            return await sharedRepository.GetAreasByCityId(cityId);
        }

        public async Task<IList<Pincode>> GetPincodesByAreaId(int areaId)
        {
            return await sharedRepository.GetPincodesByAreaId(areaId);
        }

        public async Task<IList<Locality>> GetLocalitiesByPincode(int pincodeId)
        {
            return await sharedRepository.GetLocalitiesByPincode(pincodeId);
        }
    }
}
