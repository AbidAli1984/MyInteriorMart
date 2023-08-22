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
        Task<IList<Location>> GetLocalitiesByCityId(int cityId);
        Task<IList<Pincode>> GetPincodesByLocalityId(int areaId);
        Task<IList<Area>> GetAreasByPincodeId(int pincodeId);
        #endregion


        #region Address Info
        Task<Country> GetCountryByCountryId(int countryId);
        Task<State> GetStateByStateId(int stateId);
        Task<City> GetCityByCityId(int cityId);
        Task<Location> GetLocalityByLocalityId(int localityId);
        Task<Pincode> GetPincodeByPincodeId(int pincodeId);
        Task<Area> GetAreaByAreaId(int areaId);
        Task<Location> GetLocalityByLocalityName(string locality);
        Task<Pincode> GetPincodeByPinNumber(int pinNumber);
        Task<Area> GetAreaByAreaName(string area);
        #endregion

        #region Company Info
        Task<IList<NatureOfBusiness>> GetNatureOfBusinesses();
        Task<IList<Designation>> GetDesignations();
        #endregion

        Task<IList<Religion>> GetReligions();
        Task<IList<Caste>> GetCastesByReligionId(int religionId);
    }
}
