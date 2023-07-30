using BOL.SHARED;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface ISharedService
    {
        Task AddAsync(object data);
        Task UpdateAsync(object data);

        #region Profile Info
        Task<IList<Qualification>> GetQualifications();
        Task<IList<Country>> GetCountries();
        Task<IList<State>> GetStatesByCountryId(int countryId = 101);
        Task<IList<City>> GetCitiesByStateId(int stateId);
        Task<IList<Station>> GetAreasByCityId(int cityId);
        Task<IList<Pincode>> GetPincodesByAreaId(int areaId);
        Task<IList<Locality>> GetLocalitiesByPincode(int pincodeId);
        #endregion


        #region Address Info
        Task<Country> GetCountryByCountryId(int countryId);
        Task<State> GetStateByStateId(int stateId);
        Task<City> GetCityByCityId(int cityId);
        Task<Station> GetAreaByAreaId(int areaId);
        Task<Pincode> GetPincodeByPincodeId(int pincodeId);
        Task<Locality> GetLocalityByLocalityId(int localityId);
        Task<Station> GetAreaByAreaName(string area);
        Task<Pincode> GetPincodeByPinNumber(int pinNumber);
        Task<Locality> GetLocalityByLocalityName(string localityName);
        #endregion

        #region Company Info
        Task<IList<NatureOfBusiness>> GetNatureOfBusinesses();
        Task<IList<Designation>> GetDesignations();
        #endregion
    }
}
